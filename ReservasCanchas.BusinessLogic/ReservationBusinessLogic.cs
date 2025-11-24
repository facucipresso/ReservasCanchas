using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ReservationBusinessLogic
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public ReservationBusinessLogic(ReservationRepository reservationRepository, UserBusinessLogic usurioBusinessLogic, ComplexBusinessLogic complexBusinessLogic, FieldBusinessLogic fieldBusinessLogic)
        {
            _reservationRepository = reservationRepository;
            _userBusinessLogic = usurioBusinessLogic;
            _complexBusinessLogic = complexBusinessLogic;
            _fieldBusinessLogic = fieldBusinessLogic;
        }

        public async Task<List<ReservationForUserResponseDTO>> GetReservationsByUserIdAsync()
        {
            // el usuario tiene que existir
            // obtengo el id del usuario desde el token
            var userId = 1; //_authService.GetUserIdFromToken();

            var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);
            return reservations.Select(ReservationMapper.ToReservationForUserDTO).ToList();
        }

        public async Task<ReservationForUserResponseDTO> GetReservationByIdAsync(int reservationId)
        {
            //obtenemos rol y id del usuario desde el token
            var userRol = Rol.Usuario; // _authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();
            var reservation = await GetReservationWithRelationsOrThrow(reservationId);
            //El admin puede ver cualquier reserva de canchas de su complejo o realizada por el, el usuario puede ver las reservas que realizo
            if (userRol == Rol.AdminComplejo && userId == reservation.Field.Complex.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }
            if (userRol == Rol.AdminComplejo && userId == reservation.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }

            if (userRol == Rol.Usuario && userId == reservation.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }

            throw new BadRequestException($"No tiene permisos para ver la reserva con id {reservationId}");
        }

        public async Task<List<ReservationResponseDTO>> GetReservationsByComplexIdAsync(int complexId)
        {
            //user y rol desde el token
            var userId = 1; //_authService.GetUserIdFromToken();
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var reservations = await _reservationRepository.GetReservationsByComplexIdAsync(complexId);

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ValidateOwnerShip(complex, userId);


            var reservationsDTO = reservations.Select(ReservationMapper.ToReservationResponseDTO).ToList();

            return reservationsDTO;

        }

        public async Task<List<ReservationResponseDTO>> GetReservationsByFieldIdAsync(int fieldId)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // resuelto
            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(fieldId);

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ValidateOwnerShip(field.Complex, userId);

            var reservations = await _reservationRepository.GetReservationsByFieldId(fieldId);

            return reservations.Select(ReservationMapper.ToReservationResponseDTO).ToList();
        }

        public async Task<DailyReservationsForComplexResponseDTO> GetReservationsByDateForComplexAsync(int complexId, DateOnly date)
        {

            var complex = await _complexBusinessLogic.GetComplexWithFieldsOrThrow(complexId);

            // valido la fecha
            if (date < DateOnly.FromDateTime(DateTime.Today))
                throw new BadRequestException("La fecha no puede ser anterior al día actual.");

            WeekDay requestWeekDay = _complexBusinessLogic.ConvertToWeekDay(date);

            var fields = complex.Fields;

            var dailyReservationForComplexResponse = new DailyReservationsForComplexResponseDTO
            {
                ComplexId = complexId,
                Date = date
            };

            foreach (Field field in fields)
            {
                var reservationsForField = await _reservationRepository.GetReservationsByFieldId(field.Id);
                var recurringBlocksForField = field.RecurringCourtBlocks.Where(b => b.WeekDay == requestWeekDay).ToList();
                var reservationsForFieldFiltered = reservationsForField.Where
                    (r => r.Date == date && (r.ReservationState == ReservationState.Pendiente || r.ReservationState == ReservationState.Aprobada)).ToList();

                var reservationsForFieldDTO = new ReservationsForFieldDTO
                {
                    FieldId = field.Id
                };

                var reservationsHoursForField = reservationsForFieldFiltered.Select(r => r.InitTime).ToList();
                reservationsForFieldDTO.ReservedHours.AddRange(reservationsHoursForField);

                foreach (var block in recurringBlocksForField)
                {
                    var current = block.InitHour;
                    while (current < block.EndHour)
                    {
                        reservationsForFieldDTO.ReservedHours.Add(current);
                        current = current.AddHours(1);
                    }
                }

                reservationsForFieldDTO.ReservedHours = reservationsForFieldDTO.ReservedHours.OrderBy(h => h).ToList();
                dailyReservationForComplexResponse.FieldsWithReservedHours.Add(reservationsForFieldDTO);
            }

            return dailyReservationForComplexResponse;
        }

        public async Task<CreateReservationResponseDTO> CreateReservationAsync(CreateReservationRequestDTO request, string uploadPath)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            _userBusinessLogic.ValidateUserState(user);

            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(request.FieldId);
            _fieldBusinessLogic.ValidateStatusField(field);

            // obtengo el complejo
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            _complexBusinessLogic.ValidateAccessForBasicUser(complex);

            bool isBlock = request.ReservationType == ReservationType.Bloqueo;
            // Si es un bloqueo → solo admin complejo o super
            if (isBlock)
            {
                _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            }

            // que no sea una fecha pasada y q no sea una reserva para más de 7 días despues del actual
            if (request.Date < DateOnly.FromDateTime(DateTime.Today) || request.Date > DateOnly.FromDateTime(DateTime.Today).AddDays(7))
                throw new BadRequestException("No se puede realizar una reserva para un dia anterior al actual o para más de 7 días posteriores al actual");

            var weekDay = _complexBusinessLogic.ConvertToWeekDay(request.Date);

            // me traigo el timeSlto de ese dia
            var timeSlot = complex.TimeSlots.FirstOrDefault(ts => ts.WeekDay == weekDay);

            // lo valido
            if (timeSlot == null)
                throw new BadRequestException("El complejo no tiene horarios configurados para este día.");

            //seria la hora de finalizacion de la reserva
            var endTime = request.InitTime.AddHours(1);

            // la reserva tiene que estar dentro del horario del timeslot de ese dia
            if (request.InitTime < timeSlot.InitTime || endTime > timeSlot.EndTime)
                throw new BadRequestException("El horario está fuera del horario de atención del complejo.");

            var existingReservations = await GetReservationsByDateForComplexAsync(complex.Id, request.Date);

            var reservationsForField = existingReservations.FieldsWithReservedHours.First(f => f.FieldId == field.Id);

            var existingOverlapping = reservationsForField.ReservedHours.Any(h => h == request.InitTime);

            if (existingOverlapping)
                throw new BadRequestException($"La cancha con id ${field.Id} ya esta reservada en ese horario");

            var reservation = new Reservation
            {
                UserId = userId,
                FieldId = field.Id,
                Date = request.Date,
                InitTime = request.InitTime,
                CreationDate = DateTime.Now,
                PayType = isBlock ? null : request.PayType,
                TotalPrice = isBlock ? null : field.HourPrice,
                PricePaid = isBlock ? null : request.PricePaid,
                BlockReason = isBlock ? request.BlockReason : null,
                ReservationType = isBlock ? ReservationType.Bloqueo : ReservationType.Partido,
                ReservationState = isBlock ? ReservationState.Aprobada : ReservationState.Pendiente,
            };

            await _reservationRepository.CreateReservationAsync(reservation);

            if (!isBlock)
            {
                var voucherPath = await ValidateAndSavePaymentVoucher(request.Image, uploadPath, reservation.Id);
                reservation.VoucherPath = voucherPath;
                await _reservationRepository.SaveAsync();
            }

            return ReservationMapper.ToCreateReservationResponseDTO(reservation);
        }

        public async Task ChangeStateReservationAsync(int reservationId, ChangeStateReservationRequestDTO request)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            _userBusinessLogic.ValidateUserState(user);

            var reservation = await GetReservationWithRelationsOrThrow(reservationId);

            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(reservation.FieldId);
            _fieldBusinessLogic.ValidateStatusField(field);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            _complexBusinessLogic.ValidateAccessForBasicUser(complex);

            bool changed = false;

            if((userRol == Rol.Usuario && userId == reservation.UserId) || (userRol == Rol.AdminComplejo && userId == reservation.UserId))
            { //AdminComplejo HACE UNA ACCIÓN COMO USUARIO NORMAL
                if (userId == reservation.UserId)
                {
                    var reservationDateTime = reservation.Date.ToDateTime(reservation.InitTime);
                    var diff = reservationDateTime - DateTime.Now;
                    if (reservation.ReservationState == ReservationState.Aprobada && request.newState == ReservationState.CanceladoConDevolucion
                        && diff.TotalHours >= 4)
                    {
                        reservation.ReservationState = request.newState;
                        changed = true;
                    }
                    else if(reservation.ReservationState == ReservationState.Aprobada && request.newState == ReservationState.CanceladoSinDevolucion)
                    {
                        reservation.ReservationState = request.newState;
                        changed = true;
                    }
                }
            }
            if(userRol == Rol.AdminComplejo && complex.UserId == userId)
            {//AdminComplejo HACE UNA ACCIÓN COMO DUEÑO DEL COMPLEJO
                if (reservation.ReservationState == ReservationState.Pendiente 
                    && (request.newState == ReservationState.Aprobada || request.newState == ReservationState.Rechazada))
                {
                    reservation.ReservationState = request.newState;
                    changed = true;
                }else if(reservation.ReservationState == ReservationState.Aprobada && request.newState == ReservationState.CanceladoPorAdmin)
                {
                    if (string.IsNullOrWhiteSpace(request.CancelationReason))
                        throw new BadRequestException($"Para cancelar una reserva aprobada debes incluir la razón de cancelación");

                    reservation.ReservationState = request.newState;
                    changed = true;
                }
            }

            if (!changed)
                throw new BadRequestException($"No puedes cambiar la reserva al estado {request.newState}");
            await _reservationRepository.SaveAsync();
        }

        public async Task<Reservation?> GetReservationWithRelationsOrThrow(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdWithRelationsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"La reserva con id {reservationId} no fue encontrada");
            return reservation;
        }

        private async Task<string> ValidateAndSavePaymentVoucher(IFormFile image, string uploadPath, int reservationId)
        {
            if (image == null || image.Length == 0)
                throw new BadRequestException("La imagen es obligatoria");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var ext = Path.GetExtension(image.FileName).ToLower();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                throw new BadRequestException("Formato de imagen inválido");

            const long maxBytes = 5 * 1024 * 1024; // 5MB
            if (image.Length > maxBytes)
                throw new BadRequestException("La imagen excede el tamaño máximo permitido (5MB).");

            // Generar nombre único
            var fileName = $"payment_voucher_{reservationId}{ext}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await image.CopyToAsync(stream);

            return $"uploads/reservations/{fileName}";
        }
    }
}
