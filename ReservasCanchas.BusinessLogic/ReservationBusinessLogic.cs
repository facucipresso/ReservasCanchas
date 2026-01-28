using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
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
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ReservationBusinessLogic
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly RedisRepository _redisRepository;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly FieldBusinessLogic _fieldBusinessLogic;
        private readonly NotificationBusinessLogic _notificationBusinessLogic;
        private readonly AuthService _authService;

        public ReservationBusinessLogic(ReservationRepository reservationRepository, RedisRepository redisRepository, 
            UserBusinessLogic usurioBusinessLogic, ComplexBusinessLogic complexBusinessLogic, 
            FieldBusinessLogic fieldBusinessLogic, NotificationBusinessLogic notificationBusinessLogic, AuthService authService)
        {
            _reservationRepository = reservationRepository;
            _redisRepository = redisRepository;
            _userBusinessLogic = usurioBusinessLogic;
            _complexBusinessLogic = complexBusinessLogic;
            _fieldBusinessLogic = fieldBusinessLogic;
            _notificationBusinessLogic = notificationBusinessLogic;
            _authService = authService;
        }

        public async Task<List<ReservationForUserResponseDTO>> GetReservationsByUserIdAsync()
        {
            // el usuario tiene que existir
            // obtengo el id del usuario desde el token
            var userId = _authService.GetUserId();

            var reservations = await _reservationRepository.GetReservationsByUserIdAsync(userId);
            return reservations.Select(ReservationMapper.ToReservationForUserDTO).ToList();
        }

        public async Task<ReservationForUserResponseDTO> GetReservationByIdAsync(int reservationId)
        {
            //obtenemos rol y id del usuario desde el token
            var userRol = _authService.GetUserRole();
            var userId = _authService.GetUserId();
            var reservation = await GetReservationWithRelationsOrThrow(reservationId);
            //El admin puede ver cualquier reserva de canchas de su complejo o realizada por el, el usuario puede ver las reservas que realizo
            if (userRol == "AdminComplejo" && userId == reservation.Field.Complex.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }
            if (userRol == "AdminComplejo" && userId == reservation.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }

            if (userRol == "Usuario" && userId == reservation.UserId)
            {
                return ReservationMapper.ToReservationForUserDTO(reservation);
            }

            throw new BadRequestException($"No tiene permisos para ver la reserva con id {reservationId}");
        }

        public async Task<List<ReservationResponseDTO>> GetReservationsByComplexIdAsync(int complexId)
        {
            //user y rol desde el token
            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var reservations = await _reservationRepository.GetReservationsByComplexIdAsync(complexId);

            if (userRol != "SuperAdmin")
                _complexBusinessLogic.ValidateOwnerShip(complex, userId);


            var reservationsDTO = reservations.Select(ReservationMapper.ToReservationResponseDTO).ToList();

            return reservationsDTO;

        }

        public async Task<List<ReservationResponseDTO>> GetReservationsByFieldIdAsync(int fieldId)
        {
            var userRol = _authService.GetUserRole();
            var userId = _authService.GetUserId();

            // resuelto
            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(fieldId);

            if (userRol != "SuperAdmin")
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

            DateOnly nextDate = date.AddDays(1);

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
                    (r => (r.Date == date || (r.Date == nextDate && r.InitTime.Hour < 2)) && (r.ReservationState == ReservationState.Pendiente || r.ReservationState == ReservationState.Aprobada)).ToList();

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

        public async Task<CheckoutInfoDTO> GetCheckoutInfoAsync(string reservationProcessId)
        {
            var userId = _authService.GetUserIdOrNull();

            string checkoutKey = $"checkout:{reservationProcessId}";
            string checkoutInfo = await _redisRepository.GetValueAsync(checkoutKey);
            if (checkoutInfo == null)
            {
                throw new NotFoundException("No se encontró información de checkout para el proceso de reserva proporcionado.");
            }

            var checkoutInfoDTO = System.Text.Json.JsonSerializer.Deserialize<CheckoutInfoDTO>(checkoutInfo);

            if(checkoutInfoDTO.UserId != userId)
            {
                throw new ForbiddenException("No tienes permiso para visualizar este proceso de reserva");
            }
            return checkoutInfoDTO;
        }

        public async Task<ReservationProcessResponseDTO> CreateReservationProcessAsync(ReservationProcessRequestDTO request)
        {
            var userId = _authService.GetUserId();
            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            await _userBusinessLogic.ValidateUserState(user);

            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(request.FieldId);
            _fieldBusinessLogic.ValidateStatusField(field);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(request.ComplexId);
            _complexBusinessLogic.ValidateAccessForBasicUser(complex);

            // que no sea una fecha pasada y q no sea una reserva para más de 7 días despues del actual
            if (request.Date < DateOnly.FromDateTime(DateTime.Today) || request.Date > DateOnly.FromDateTime(DateTime.Today).AddDays(7))
                throw new BadRequestException("No se puede realizar una reserva para un dia anterior al actual o para más de 7 días posteriores al actual");

            var weekDay = _complexBusinessLogic.ConvertToWeekDay(request.Date);

            var timeSlot = complex.TimeSlots.FirstOrDefault(ts => ts.WeekDay == weekDay);
            Console.WriteLine($"Dia: {timeSlot.WeekDay}, Horarios: {timeSlot.InitTime} y {timeSlot.EndTime}");
            if (timeSlot.InitTime == timeSlot.EndTime)
                throw new BadRequestException($"El complejo está cerrado los días {weekDay}.");

            bool crossesMidnight = timeSlot.EndTime < timeSlot.InitTime;

            if (crossesMidnight)
            {
                if (request.StartTime >= timeSlot.EndTime && request.StartTime < timeSlot.InitTime)
                {
                    throw new BadRequestException("El horario está fuera del horario de atención del complejo .");
                }
            }
            else
            {
                if (request.StartTime < timeSlot.InitTime || request.StartTime >= timeSlot.EndTime)
                {
                    throw new BadRequestException("El horario está fuera del horario de atención del complejo.");
                }
            }


            string userKey = $"user:{userId}";

            string? reservationProcessForUser = await _redisRepository.GetValueAsync(userKey);

            if (!string.IsNullOrEmpty(reservationProcessForUser))
            {
                return new ReservationProcessResponseDTO
                {
                    ExistReservationProcessForUser = true,
                    ReservationProcessId = reservationProcessForUser
                };
            }

            DateOnly finalDate = request.Date;
            if(crossesMidnight && (request.StartTime.Hour == 0 || request.StartTime.Hour == 1))
            {
                finalDate = request.Date.AddDays(1);
            }

            string processGuid = Guid.NewGuid().ToString();
            string lockKey = $"lock:cancha:{request.FieldId}:{request.Date:yyyyMMdd}:{request.StartTime:HHmm}";
            string checkoutKey = $"checkout:{processGuid}";
            var expiry = TimeSpan.FromMinutes(15);

            bool lockAcquired = await _redisRepository.LockSlotIfNotExistAsync(lockKey, processGuid, expiry);
            bool reservationExists = await _reservationRepository.ExistsApprovedReservationAsync(request.FieldId, request.Date, request.StartTime);
            if (!lockAcquired) {
                throw new ConflictException("El horario seleccionado ya está siendo reservado por otro usuario. Por favor, intenta más tarde o selecciona otro horario.");
            }
            if (reservationExists)
            {
                throw new ConflictException("El horario seleccionado ya se encuentra reservado.");
            }

            DateTime expirationTime = DateTime.UtcNow.Add(expiry);

            TimeOnly globalDayStart = new TimeOnly(8, 0);

            bool ilumination = false;

            if (field.Ilumination)
            {
                bool isNightTime = request.StartTime >= complex.StartIlumination;
                bool isEarlyMorningTime = request.StartTime < globalDayStart;
                if(isNightTime || isEarlyMorningTime)
                {
                    ilumination = true;
                }
            }


            CheckoutInfoDTO checkoutInfo = new CheckoutInfoDTO
            {
                UserId = userId,
                ComplexId = request.ComplexId,
                FieldId = request.FieldId,
                ExpirationTime = expirationTime,
                Date = finalDate,
                InitTime = request.StartTime,
                Ilumination = ilumination
            };

            await _redisRepository.SetCheckoutContextAsync(checkoutKey, checkoutInfo, expiry);
            await _redisRepository.SetUserIdAsync(userKey, processGuid, expiry);
            return new ReservationProcessResponseDTO
            {
                ExistReservationProcessForUser = false,
                ReservationProcessId = processGuid
            };
        }

        public async Task DeleteReservationProcessAsync(string reservationProcessId)
        {
            var userId = _authService.GetUserId();
            var userKey = $"user:{userId}";
            string? currentProcessId = await _redisRepository.GetValueAsync(userKey);
            if (string.IsNullOrEmpty(currentProcessId))
            {
                return;
            }

            if (currentProcessId != reservationProcessId)
            {
                throw new ForbiddenException("No tienes permiso para eliminar este proceso de reserva.");
            }

            string checkoutKey = $"checkout:{reservationProcessId}";

            string checkoutInfo = await _redisRepository.GetValueAsync(checkoutKey);

            CheckoutInfoDTO checkoutInfoJson = JsonSerializer.Deserialize<CheckoutInfoDTO>(checkoutInfo);

            string lockKey = $"lock:cancha:{checkoutInfoJson.FieldId}:{checkoutInfoJson.Date:yyyyMMdd}:{checkoutInfoJson.InitTime:HHmm}";

            await _redisRepository.DeleteKeyAsync(lockKey);
            await _redisRepository.DeleteKeyAsync(checkoutKey);
            await _redisRepository.DeleteKeyAsync(userKey);
        }

        public async Task<CreateReservationResponseDTO> CreateReservationAsync(CreateReservationRequestDTO request, string uploadPath)
        {
            var userRol = _authService.GetUserRole();
            var userId = _authService.GetUserId();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            await _userBusinessLogic.ValidateUserState(user);

            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(request.FieldId);
            _fieldBusinessLogic.ValidateStatusField(field);

            // obtengo el complejo
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            _complexBusinessLogic.ValidateAccessForBasicUser(complex);

            // obtengo del id del admin del complejo para la notificacion #######
            var complexAdminId = complex.UserId;

            bool isBlock = request.ReservationType == ReservationType.Bloqueo;
            // Si es un bloqueo, solo admin complejo
            if (isBlock)
            {
                _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            }

            // que no sea una fecha pasada y q no sea una reserva para más de 7 días despues del actual

            DateOnly operationalDate = request.Date;
            if (request.InitTime.Hour < 2)
            {
                operationalDate = request.Date.AddDays(-1);
            }

            if (operationalDate < DateOnly.FromDateTime(DateTime.Today) || operationalDate > DateOnly.FromDateTime(DateTime.Today).AddDays(7))
                throw new BadRequestException("No se puede realizar una reserva para un dia anterior al actual o para más de 7 días posteriores al actual");

            var weekDay = _complexBusinessLogic.ConvertToWeekDay(operationalDate);

            var timeSlot = complex.TimeSlots.FirstOrDefault(ts => ts.WeekDay == weekDay);

            if (timeSlot == null || timeSlot.InitTime == timeSlot.EndTime)
                throw new BadRequestException($"El complejo está cerrado el día operativo {weekDay}.");

            bool isValidTime = false;
            bool crossesMidnight = timeSlot.EndTime < timeSlot.InitTime; 

            if (crossesMidnight)
            {
                // CASO A: Madrugada (00, 01)
                if (request.InitTime.Hour < 2)
                {
                    // Debe ser menor al cierre (ej: 01:00 < 02:00)
                    if (request.InitTime < timeSlot.EndTime) isValidTime = true;
                }
                // CASO B: Noche normal (20:00, 21:00)
                else
                {
                    // Debe ser mayor al inicio (ej: 20:00 >= 18:00)
                    if (request.InitTime >= timeSlot.InitTime) isValidTime = true;
                }
            }
            else // Horario normal (ej: 09:00 a 23:00)
            {
                if (request.InitTime >= timeSlot.InitTime && request.InitTime < timeSlot.EndTime)
                    isValidTime = true;
            }

            if (!isValidTime)
            {
                throw new BadRequestException("El horario está fuera del horario de atención del complejo.");
            }

            var existingReservations = await GetReservationsByDateForComplexAsync(complex.Id, request.Date);

            var reservationsForField = existingReservations.FieldsWithReservedHours.First(f => f.FieldId == field.Id);

            var existingOverlapping = reservationsForField.ReservedHours.Any(h => h == request.InitTime);

            if (existingOverlapping)
                throw new BadRequestException($"La cancha con id ${field.Id} ya esta reservada en ese horario");

            TimeOnly openningTime = timeSlot.InitTime;

            decimal finalTotalPrice = field.HourPrice;

            if (field.Ilumination)
            {
                TimeOnly globalDayStart = new TimeOnly(8, 0);
                bool isNightTime = request.InitTime >= complex.StartIlumination;
                bool isEarlyMorningTime = request.InitTime < globalDayStart;

                if(isNightTime || isEarlyMorningTime)
                {
                    decimal surcharge = (field.HourPrice * complex.AditionalIlumination) / 100;
                    finalTotalPrice += surcharge;
                }
            }

            decimal expectedPrice = finalTotalPrice;

            if(request.PayType == PayType.PagoParcial)
            {
                expectedPrice = (finalTotalPrice * complex.PercentageSign) / 100;
            }

            if (!isBlock)
            {
                
                decimal backendPrice = Math.Round(expectedPrice, 2);
                decimal frontendPrice = Math.Round(request.PricePaid, 2);

                if (backendPrice != frontendPrice)
                {
                    throw new BadRequestException($"Error de validación: El monto enviado (${frontendPrice}) no coincide con el calculado por el servidor (${backendPrice}).");
                }
            }



            var reservation = new Reservation
            {
                UserId = userId,
                FieldId = field.Id,
                Date = request.Date,
                InitTime = request.InitTime,
                CreationDate = DateTime.UtcNow,
                PayType = isBlock ? null : request.PayType,
                TotalPrice = finalTotalPrice,
                PricePaid = isBlock ? 0 : expectedPrice,
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

                var notificationForAdmin = new Notification 
                {
                    UserId = complexAdminId,
                    Title = "Nueva reserva pendiente",
                    Message = $"El usuario {user.Name} realizó una reserva en la cancha '{field.Name}' para el {request.Date} a las {request.InitTime:HH\\:mm}."
                };
                var notificationForUser = new Notification
                {
                    UserId = userId,
                    Title = "Reserva creada con éxito",
                    Message = $"Has creado una reserva en la cancha '{field.Name}' para el {request.Date} a las {request.InitTime:HH\\:mm}. " +
                              $"Tu reserva está pendiente de aprobación por el administrador del complejo."
                };
                await _notificationBusinessLogic.CreateNotificationAsync(notificationForUser);
                await _notificationBusinessLogic.CreateNotificationAsync(notificationForAdmin);
            }
            else
            {
                // notificacion de bloqueo realizado
                var notificationForAdmin = new Notification
                {
                    UserId = userId,
                    Title = "Bloqueo de cancha realizado",
                    Message = $"Has bloqueado la cancha '{field.Name}' para el {request.Date} a las {request.InitTime:HH\\:mm}." +
                              $"Motivo del bloqueo: {request.BlockReason}"
                };
                await _notificationBusinessLogic.CreateNotificationAsync(notificationForAdmin);
            }

            if (!string.IsNullOrEmpty(request.ProcessId))
            {
                string lockKey = $"lock:cancha:{request.FieldId}:{request.Date:yyyyMMdd}:{request.InitTime:HHmm}";
                string checkoutKey = $"checkout:{request.ProcessId}";
                string userKey = $"user:{userId}";
                await _redisRepository.DeleteKeyAsync(lockKey);
                await _redisRepository.DeleteKeyAsync(checkoutKey);
                await _redisRepository.DeleteKeyAsync(userKey);
            }

                return ReservationMapper.ToCreateReservationResponseDTO(reservation);
        }

        /*public async Task ChangeStateReservationAsync(int reservationId, ChangeStateReservationRequestDTO request)
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

                    if (changed)
                    {
                        // si el estado cambio genero la notificacion
                        var notification = new Notification
                        {
                            UserId = complex.UserId,
                            Title = "Reserva cancelada",
                            Message = $"La reserva con id {reservation.Id} fue cancelada por el usuario {user.Name} {user.LastName}.",
                            ReservationId = reservation.Id,
                        };

                        await _notificationBusinessLogic.CreateNotificationAsync(notification);
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
                    reservation.CancellationReason = request.CancelationReason;
                    changed = true;
                }

                if (changed)
                {
                    //aca creo que deberia generar la otra notificacion PERO ESTA VEZ DEL COMPLEXADMIN AL USUARIO
                    var notification = new Notification
                    {
                        UserId = reservation.UserId,
                        Title = "Reserva cancelada",
                        Message = $"La reserva con id {reservation.Id} fue cancelada. Motivo: {reservation.CancellationReason}.",
                        ReservationId = reservation.Id,
                    };

                    await _notificationBusinessLogic.CreateNotificationAsync(notification);
                }

            }

            if (!changed)
                throw new BadRequestException($"No puedes cambiar la reserva al estado {request.newState}");
            await _reservationRepository.SaveAsync();
        }*/

        public async Task ChangeStateReservationAsync(int reservationId, ChangeStateReservationRequestDTO request)
        {
            var userRol =_authService.GetUserRole();
            var userId = _authService.GetUserId();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            await _userBusinessLogic.ValidateUserState(user);

            var reservation = await GetReservationWithRelationsOrThrow(reservationId);

            var field = await _fieldBusinessLogic.GetFieldWithRelationsOrThrow(reservation.FieldId);
            _fieldBusinessLogic.ValidateStatusField(field);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            _complexBusinessLogic.ValidateAccessForBasicUser(complex);

            bool changed = false;

            //  CANCELACIÓN REALIZADA POR EL USUARIO (o admin actuando como usuario)
            if ((userRol == "Usuario" && userId == reservation.UserId) ||
                (userRol == "AdminComplejo" && userId == reservation.UserId))
            {
                var reservationDateTime = reservation.Date.ToDateTime(reservation.InitTime);
                var diff = reservationDateTime - DateTime.Now;

                if (reservation.ReservationState == ReservationState.Aprobada &&
                    request.newState == ReservationState.CanceladoConDevolucion &&
                    diff.TotalHours >= 4)
                {
                    reservation.ReservationState = request.newState;
                    reservation.CancellationReason = null;
                    changed = true;
                }
                else if (reservation.ReservationState == ReservationState.Aprobada &&
                         request.newState == ReservationState.CanceladoSinDevolucion)
                {
                    reservation.ReservationState = request.newState;
                    reservation.CancellationReason = null;
                    changed = true;
                }

                if (changed)
                {
                    await _notificationBusinessLogic.CreateNotificationAsync(new Notification
                    {
                        UserId = complex.UserId,
                        Title = "Reserva cancelada por usuario",
                        Message = $"El usuario {user.Name} {user.LastName} canceló la reserva con id {reservation.Id}.",
                        ReservationId = reservation.Id,
                    });
                }
            }

            // ACCIONES DEL ADMIN DEL COMPLEJO
            if (userRol == "AdminComplejo" && complex.UserId == userId)
            {
                if (reservation.ReservationState == ReservationState.Pendiente &&
                   (request.newState == ReservationState.Aprobada || request.newState == ReservationState.Rechazada))
                {
                    reservation.ReservationState = request.newState;
                    reservation.CancellationReason = null;
                    changed = true;
                }
                else if (reservation.ReservationState == ReservationState.Aprobada &&
                         request.newState == ReservationState.CanceladoPorAdmin)
                {
                    if (string.IsNullOrWhiteSpace(request.CancelationReason))
                        throw new BadRequestException("Para cancelar una reserva aprobada debes incluir la razón de cancelación");

                    reservation.ReservationState = request.newState;
                    reservation.CancellationReason = request.CancelationReason;
                    changed = true;
                }

                if (changed)
                {
                    await _notificationBusinessLogic.CreateNotificationAsync(new Notification
                    {
                        UserId = reservation.UserId,
                        Title = "Tu reserva fue cancelada",
                        Message = $"La reserva con id {reservation.Id} fue cancelada por el administrador. " +
                                  (reservation.CancellationReason != null ?
                                    $"Motivo: {reservation.CancellationReason}" :
                                    ""),
                        ReservationId = reservation.Id,
                    });
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

        public async Task ApproveReservationAsync(ApproveReservationRequestDTO request)
        {
            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            var reservation = await GetReservationWithRelationsOrThrow(request.ReservationId);

            // Validaciones
            ValidateReservationApproval(reservation, userId, userRol);

            reservation.ReservationState = ReservationState.Aprobada;
            await _reservationRepository.SaveAsync();

            // creo la notificacion 
            var notification = new Notification
            {
                UserId = reservation.UserId,
                Title = "Tu reserva fue aprobada",
                Message = $"Tu reserva en '{reservation.Field.Name}' para el {reservation.Date} a las {reservation.InitTime:HH\\:mm} fue aprobada.",
                ReservationId = reservation.Id,
                ComplexId = reservation.Field.ComplexId
            };

            await _notificationBusinessLogic.CreateNotificationAsync(notification);
        }

        public async Task RejectReservationAsync(RejectReservationRequestDTO request)
        {
            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            var reservation = await GetReservationWithRelationsOrThrow(request.ReservationId);

            ValidateReservationRejection(reservation, userId, userRol);

            reservation.ReservationState = ReservationState.Rechazada;
            await _reservationRepository.SaveAsync();

            var notification = new Notification
            {
                UserId = reservation.UserId,
                Title = "Tu reserva fue rechazada",
                Message = $"La reserva fue rechazada. Motivo: {request.Reason}.",
                ReservationId = reservation.Id,
                ComplexId = reservation.Field.ComplexId
            };

            await _notificationBusinessLogic.CreateNotificationAsync(notification);
        }

        private void ValidateReservationApproval(Reservation reservation, int userId, string userRol)
        {
            // debe existir
            if (reservation == null)
                throw new BadRequestException("La reserva no existe");

            // debe estar pendiente
            if (reservation.ReservationState != ReservationState.Pendiente)
                throw new BadRequestException("Solo se pueden aprobar reservas pendientes");

            // debe ser admin del complejo o superuser
            if (userRol == "AdminComplejo")
            {
                if (reservation.Field.Complex.UserId != userId)
                    throw new BadRequestException("No tiene permisos para aprobar reservas de este complejo");
            }
            else if (userRol != "SuperAdmin")
            {
                throw new BadRequestException("No tiene permisos para aprobar reservas");
            }
        }

        private void ValidateReservationRejection(Reservation reservation, int userId, string userRol)
        {
            // debe existir
            if (reservation == null)
                throw new BadRequestException("La reserva no existe");

            // debe estar pendiente
            if (reservation.ReservationState != ReservationState.Pendiente)
                throw new BadRequestException("Solo se pueden aprobar reservas pendientes");

            // debe ser admin del complejo o superuser
            if (userRol == "AdminComplejo")
            {
                if (reservation.Field.Complex.UserId != userId)
                    throw new BadRequestException("No tiene permisos para aprobar reservas de este complejo");
            }
            else if (userRol != "SuperAdmin")
            {
                throw new BadRequestException("No tiene permisos para aprobar reservas");
            }
        }



    }
}
