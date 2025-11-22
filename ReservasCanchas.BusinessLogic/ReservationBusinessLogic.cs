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
        private readonly UsuarioBusinessLogic _usuarioBusinessLogic;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ComplexRepository _complexRepository;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public ReservationBusinessLogic(ReservationRepository reservationRepository, UsuarioBusinessLogic usurioBusinessLogic, UsuarioRepository usuarioRepository, ComplexRepository complexRepository, ComplexBusinessLogic complexBusinessLogic, FieldBusinessLogic fieldBusinessLogic)
        {
            _reservationRepository = reservationRepository;
            _usuarioBusinessLogic = usurioBusinessLogic;
            _usuarioRepository = usuarioRepository;
            _complexRepository = complexRepository;
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

        public async Task<List<ReservationForComplexResponseDTO>> GetReservationsByComplexIdAsync(int complexId)
        {
            //user y rol desde el token
            var userId = 1; //_authService.GetUserIdFromToken();
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var reservations = await _reservationRepository.GetReservationsByComplexIdAsync(complexId);

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ValidateOwnerShip(complex, userId);


            var reservationsDTO = reservations.Select(ReservationMapper.ToReservationForComplexResponseDTO).ToList();

            return reservationsDTO;

        }

        public async Task<List<ReservationForFieldResponseDTO>> GetReservationsByFieldIdAsync(int fieldId)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // resuelto
            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(fieldId);

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ValidateOwnerShip(field.Complex, userId);

            var reservations = await _reservationRepository.GetReservationsByFieldId(fieldId);

            return reservations.Select(ReservationMapper.ToReservationForFieldResponseDTO).ToList();
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

        public async Task ChangeStateReservationAsync(int complexId, int fieldId, int reservationId, ChangeStateReservationRequest request)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // Esto me da un usuario Dto, que si no existe o si no esta habilitado me tira solo el error
            var user = await _usurioBusinessLogic.GetByIdIfIsEnabled(userId);

            // obtengo el complejo
            var complex = await _complexBusinessLogic.GetComplexByIdIfIsActiveAsync(complexId);

            // si no existe me da error o si no esta activo
            if (complex == null || complex.Active == false) throw new BadRequestException($"No existe un complejo con el id {complexId} o no se encuentra activo el complejo");

            // si no es SuperUser o ComplexAdmin no puede 
            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            // si no esta habilitado me da error
            _complexBusinessLogic.ComplexValidityStateCheck(complex);

            // ya checkea que este activa la cancha y que pertenezca al complejo, osea tambien tiene que existir el complejo
            var field = await _fieldBusinessLogic.FieldValidityCheck(fieldId, complexId);

            // obtengo y ckeckeo que exista la reserva
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

            if (reservation == null)
                throw new BadRequestException($"No existe la reserva con el id {reservationId}");

            reservation.ReservationState = ReservationState.CanceladoSinDevolucion;
            await _reservationRepository.UpdateAsync(reservation);


        }

        public async Task CancelReservationByIdAsync(int reservationId, CancelReservationDTO cancelReservarionDTO)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

            if (reservation == null)
                throw new BadRequestException($"No existe la reserva con el id {reservationId}");

            // el admin puede cancelar
            if (userRol == Rol.AdminComplejo)
            {
                reservation.BlockReason = cancelReservarionDTO.ReasonCancel;
                reservation.ReservationState = ReservationState.CanceladoPorAdmin;
            }


            if(userRol != Rol.SuperAdmin)
            {
                if(userId == reservation.UserId)
                    throw new BadRequestException($"Las reservas solo pueden ser eliminadas por el usuario o el administrador del complejo que la creo");
            }

            var now = DateTime.Now;
            var reservationStart = reservation.Date.ToDateTime(reservation.InitTime);

            if (now >= reservationStart)
                throw new BadRequestException("No se puede cancelar una reserva que ya comenzó o ya pasó.");

            var timeToMatch = reservationStart - now;

            if (timeToMatch.TotalHours > 1)
            {
                // Cancelación hecha con más de 1 hora de anticipación
                reservation.ReservationState = ReservationState.CanceladoConDevolucion;
            }

            if(timeToMatch.TotalHours < 1)
            {
                // Cancelación hecha faltando 1 hora o menos
                reservation.ReservationState = ReservationState.CanceladoSinDevolucion;
            }

            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task<CreateReservationResponseDTO> CreateReservationAsync(int complexId, int fieldId, CreateReservationRequestDTO request)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // Esto me da un usuario Dto, que si no existe o si no esta habilitado me tira solo el error
            var user = await _usurioBusinessLogic.GetByIdIfIsEnabled(userId);

            // obtengo el complejo
            var complex = await _complexBusinessLogic.GetComplexByIdIfIsActiveAsync(complexId);

            // si no existe me da error o si no esta activo
            if (complex == null || complex.Active == false) throw new BadRequestException($"No existe un complejo con el id {complexId} o no se encuentra activo el complejo");

            // si no esta habilitado me da error
            _complexBusinessLogic.ComplexValidityStateCheck(complex);

            // ya checkea que este activa la cancha y que pertenezca al complejo, osea tambien tiene que existir el complejo
            var field = await _fieldBusinessLogic.FieldValidityCheck(fieldId, complexId);

            // que no sea una fecha pasada
            if (request.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BadRequestException("La fecha no puede ser anterior al día de hoy.");

            // convierto a nuestro enum
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

            // traigo posibles reservas existenten a ese horario
            var overlappingReservation = field.Reservations
                .Where(r => r.Date == request.Date)
                .Any(r => r.InitTime == request.InitTime && r.ReservationState != ReservationState.CanceladoConDevolucion && r.ReservationState != ReservationState.CanceladoSinDevolucion);
            
            // lo valido
            if (overlappingReservation)
                throw new BadRequestException("Ya existe una reserva en ese horario.");

            // checkeo los bloqueos recurrentes que no se solapen
            var overlappingBlock = field.RecurringCourtBlocks
                .Where(b => b.WeekDay == weekDay)
                .Any(b => request.InitTime >= b.InitHour && request.InitTime < b.EndHour);

            if (overlappingBlock)
                throw new BadRequestException("El horario está bloqueado por un bloqueo recurrente.");

            // defino el estado de la reserva segun quien la haga CHARLARLO
            var initialState = userRol == Rol.AdminComplejo || userRol == Rol.SuperAdmin
                ? ReservationState.Aprobada
                : ReservationState.Pendiente;

            var reservation = new Reservation
            {
                UserId = userId,
                FieldId = fieldId,
                Date = request.Date,
                InitTime = request.InitTime,
                CreationDate = DateTime.Now,// capaz esto puede venir del front
                PayType = request.PayType,
                TotalPrice = field.HourPrice,
                PricePaid = request.PricePaid,
                ReservationType = ReservationType.Partido,
                ReservationState = initialState,
                VoucherPath = "" // ver como manejamos esto
            };

            await _reservationRepository.CreateReservationAsync(reservation);

            return ReservationMapper.ToCreateReservationResponseDTO(reservation);
        }

        public async Task<CreateReservationResponseDTO> CreateReservationBlockingAsync(int complexId, int fieldId, ReservationBlockingRequestDto blocking)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // Esto me da un usuario Dto, que si no existe o si no esta habilitado me tira solo el error
            var user = await _usurioBusinessLogic.GetByIdIfIsEnabled(userId);

            // obtengo el complejo
            var complex = await _complexBusinessLogic.GetComplexByIdIfIsActiveAsync(complexId);

            // si no existe me da error o si no esta activo
            if (complex == null || complex.Active == false) throw new BadRequestException($"No existe un complejo con el id {complexId} o no se encuentra activo el complejo");

            // si no es SuperUser o ComplexAdmin no puede 
            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            // si no esta habilitado me da error
            _complexBusinessLogic.ComplexValidityStateCheck(complex);

            // ya checkea que este activa la cancha y que pertenezca al complejo, osea tambien tiene que existir el complejo
            var field = await _fieldBusinessLogic.FieldValidityCheck(fieldId, complexId);

            // que no sea una fecha pasada
            if (blocking.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BadRequestException("La fecha no puede ser anterior al día de hoy.");

            // convierto a nuestro enum
            var weekDay = _complexBusinessLogic.ConvertToWeekDay(blocking.Date);

            // me traigo el timeSlto de ese dia
            var timeSlot = complex.TimeSlots.FirstOrDefault(ts => ts.WeekDay == weekDay);

            // lo valido
            if (timeSlot == null)
                throw new BadRequestException("El complejo no tiene horarios configurados para este día.");

            //seria la hora de finalizacion de la reserva
            var endTime = blocking.InitTime.AddHours(1);

            // la reserva tiene que estar dentro del horario del timeslot de ese dia
            if (blocking.InitTime < timeSlot.InitTime || endTime > timeSlot.EndTime)
                throw new BadRequestException("El horario está fuera del horario de atención del complejo.");

            // traigo posibles reservas existenten a ese horario
            var overlappingReservation = field.Reservations
                .Where(r => r.Date == blocking.Date)
                .Any(r => r.InitTime == blocking.InitTime && r.ReservationState != ReservationState.CanceladoConDevolucion && r.ReservationState != ReservationState.CanceladoSinDevolucion);

            // lo valido
            if (overlappingReservation)
                throw new BadRequestException("Ya existe una reserva en ese horario.");

            // checkeo los bloqueos recurrentes que no se solapen
            var overlappingBlock = field.RecurringCourtBlocks
                .Where(b => b.WeekDay == weekDay)
                .Any(b => blocking.InitTime >= b.InitHour && blocking.InitTime < b.EndHour);

            if (overlappingBlock)
                throw new BadRequestException("El horario está bloqueado por un bloqueo recurrente.");


            var reservation = new Reservation
            {
                UserId = userId,
                FieldId = fieldId,
                Date = blocking.Date,
                InitTime = blocking.InitTime,
                CreationDate = DateTime.Now,// capaz esto puede venir del front
                ReservationType = ReservationType.Bloqueo,
                ReservationState = ReservationState.Aprobada,
            };

            await _reservationRepository.CreateReservationAsync(reservation);

            return ReservationMapper.ToCreateReservationResponseDTO(reservation);
        }

        public async Task<Reservation> GetReservationWithReviewAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationWithReviewByIdAsync(reservationId);
            if(reservation == null)
                throw new NotFoundException($"La reserva con id {reservationId} no fue encontrada");
            return reservation;
        }

        public async Task<Reservation?> GetReservationWithRelationsOrThrow(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdWithRelationsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"La reserva con id {reservationId} no fue encontrada");
            return reservation;
        }
    }
}
