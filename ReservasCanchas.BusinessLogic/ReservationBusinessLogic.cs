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
        private readonly UsuarioBusinessLogic _usurioBusinessLogic;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ComplexRepository _complexRepository;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public ReservationBusinessLogic(ReservationRepository reservationRepository, UsuarioBusinessLogic usurioBusinessLogic, UsuarioRepository usuarioRepository, ComplexRepository complexRepository, ComplexBusinessLogic complexBusinessLogic, FieldBusinessLogic fieldBusinessLogic)
        {
            _reservationRepository = reservationRepository;
            _usurioBusinessLogic = usurioBusinessLogic;
            _usuarioRepository = usuarioRepository;
            _complexRepository = complexRepository;
            _complexBusinessLogic = complexBusinessLogic;
            _fieldBusinessLogic = fieldBusinessLogic;
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
            var reservation = await _reservationRepository.GetReservationByIdReservationAsync(reservationId);

            if (reservation == null)
                throw new BadRequestException($"No existe la reserva con el id {reservationId}");

            reservation.ReservationState = ReservationState.CanceladoSinDevolucion;
            await _reservationRepository.UpdateAsync(reservation);


        }

        public async Task CancelReservationByIdAsync(int reservationId, CancelReservationDTO cancelReservarionDTO)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            var reservation = await _reservationRepository.GetReservationByIdReservationAsync(reservationId);

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
        //Cualquiera puede obtener una reserva por id, esta bien?****
        public async Task<ReservationForUserResponseDTO> GetReservationsByIdAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdReservationAsync(reservationId);
            if(reservation == null)
                throw new NotFoundException($"La reserva con id {reservationId} no existe");
            return ReservationMapper.ToReservationForUserDTO(reservation);
        }

        public async Task<List<ReservationForComplexResponseDTO>> GetReservationsForComplexAsync(int userId, int complexId)
        {
            var user = await _usuarioRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + userId + " no encontrado");
            }

            await _complexBusinessLogic.ComplexValidityExistenceCheck(complexId);

            var complex = await _complexBusinessLogic.GetComplexByIdWithReservationsAsync(complexId);

            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();

            if(userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);
            

            var reservations = complex.Fields
                .SelectMany(f => f.Reservations)
                .ToList();

            var dtoList = reservations.Select(ReservationMapper.ToReservationForComplexResponseDTO).ToList();

            return dtoList;

        }

        public async Task<DayAvailabilityResponseDTO> GetReservationsForDaysAsync(int complexId, ReservationForDayRequest reservationRequest)
        { //EN REALIDAD ESTE ES PARA CHEQUEAR LA DISPONIBILIDAD DE LA CANCHA PARA EL DIA EN Q EL USUARIO QUIERE RESERVAR.
            /* devolver algo asi.
                  "complexId": 3,
                  "date": "2025-11-20",
                  "fields": [
                    {
                      "fieldId": 1,
                      "availableHours": ["08:00", "09:00", "11:00"]
                    },
                    {
                      "fieldId": 2,
                      "availableHours": ["10:00", "12:00"]
                    }
                  ]
                }
             */
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();
            
            // Esto me da un usuario Dto, que si no existe me tira solo el error
            var user = await _usurioBusinessLogic.GetById(userId);

            //resuelto
            var complex = await _complexBusinessLogic.GetComplexByIdIfIsActiveAsync(complexId);

            if (complex == null)
                throw new BadRequestException($"No existe un complejo con el id {complexId}");

            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            // vaido la fecha
            if (reservationRequest.Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BadRequestException("La fecha no puede ser anterior al día actual.");

            // Filtrar por canchas activas, tipo de cancha y tipo de suelo
            var validFields = complex.Fields
                .Where(f =>
                    f.Active &&
                    f.FieldType == reservationRequest.FieldType &&
                    f.FloorType == reservationRequest.FloorType)
                .ToList();

            if (!validFields.Any())
                throw new NotFoundException(
                    $"No hay canchas activas en este complejo con tipo {reservationRequest.FieldType} y piso {reservationRequest.FloorType}");

            WeekDay requestWeekDay = _complexBusinessLogic.ConvertToWeekDay(reservationRequest.Date);

            // Filtro las reservas del día
            var reservations = validFields
                .SelectMany(f => f.Reservations)
                .Where(r => r.Date == reservationRequest.Date && (r.ReservationState == ReservationState.Pendiente || r.ReservationState == ReservationState.Aprobada))
                .ToList();

            // Filtro ls bloqueos recurrentes del día
            var recurringBlocks = validFields
                .SelectMany(f => f.RecurringCourtBlocks)
                .Where(b => b.WeekDay == requestWeekDay)
                .ToList();

            // ==================================================================================

            var response = new DayAvailabilityResponseDTO
            {
                ComplexId = complex.Id,
                Date = reservationRequest.Date
            };

            //ahora agrupo por cancha
            foreach (var field in validFields)
            {
                var dto = new ReservationForFieldDTO
                {
                    FieldId = field.Id
                };

                //primero agrego las reservas normales
                var fieldReservations = reservations
                    .Where(r => r.FieldId == field.Id)
                    .Select(r => r.InitTime);

                dto.ReservedHours.AddRange(fieldReservations);

                //agrego las horas segmentadas de bloqueos recurrentes
                var fieldBlocks = recurringBlocks
                    .Where(b => b.FieldId == field.Id);

                foreach (var block in fieldBlocks)
                {
                    var current = block.InitHour;
                    while(current < block.EndHour)
                    {
                        dto.ReservedHours.Add(current);
                        current = current.AddHours(1);
                    }
                }

                //ordeno las horas
                dto.ReservedHours = dto.ReservedHours
                    .OrderBy(h => h)
                    .ToList();

                response.FieldsAvailability.Add(dto);
            }

            return response;

            /*DayAvailabilityResponseDTO response = new DayAvailabilityResponseDTO
            {
                ComplexId = complex.Id,
                Date = reservationRequest.Date
            };

            response.Reservations = reservations.Select(ReservationMapper.ToDayReservationDTO).ToList();

            response.RecurringBlocks = recurringBlocks.Select(ReservationMapper.ToDayRecurringBlockDTO).ToList();

            return response;*/
        }

        public async Task<List<ReservationForFieldResponseDTO>> GetReservationsForFieldAsync(int complexId, int fieldId)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();
            
            var user = await _usurioBusinessLogic.GetById(userId);

            // resuelto
            var complex = await _complexBusinessLogic.GetComplexByIdIfIsActiveAsync(complexId);

            if (complex == null) 
                throw new BadRequestException($"No existe un complejo con el id {complexId}");


            if (userRol != Rol.SuperAdmin)
                _complexBusinessLogic.ComplexValidityAdmin(complex, userId);

            var field = complex.Fields.FirstOrDefault(f => f.Id == fieldId);

            if (field == null)
                throw new NotFoundException($"La cancha con id {fieldId} no pertenece a este complejo");

            var reservations = field.Reservations.ToList();

            return reservations
                .Select(ReservationMapper.ToReservationForFieldResponseDTO)
                .ToList();
        }

        public async Task<ActionResult<List<ReservationForUserResponseDTO>>> GetReservationsForUserAsync(int userId)
        {
            // el usuario tiene que existir
            var exist = await _usurioBusinessLogic.ExistUser(userId);

            var reservations = await _reservationRepository.GetReservationsByUserAsync(userId);

            var result = new List<ReservationForUserResponseDTO>();

            foreach (var r in reservations)
            { 
                var dtto = ReservationMapper.ToReservationForUserDTO(r);
                result.Add(dtto);
            }
            return result;
        }

        public async Task<Reservation> GetReservationWithReviewAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationWithReviewByIdAsync(reservationId);
            if(reservation == null)
                throw new NotFoundException($"La reserva con id {reservationId} no fue encontrada");
            return reservation;
        }
    }
}
