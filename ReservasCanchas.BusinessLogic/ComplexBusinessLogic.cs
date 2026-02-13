using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ReservasCanchas.BusinessLogic
{
    public class ComplexBusinessLogic
    {
        private readonly ComplexRepository _complexRepository;
        private readonly ServiceBusinessLogic _serviceBusinessLogic;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly UserRepository _userRepository;
        private readonly NotificationBusinessLogic _notificationBusinessLogic;
        private readonly AuthService _authService;

        public ComplexBusinessLogic(ComplexRepository complexRepository, ServiceBusinessLogic serviceBusinessLogic, UserBusinessLogic usuarioBusinessLogic, UserRepository userRepository,NotificationBusinessLogic notificationBusinessLogic, AuthService authService)
        {
            _complexRepository = complexRepository;
            _serviceBusinessLogic = serviceBusinessLogic;
            _userBusinessLogic = usuarioBusinessLogic;
            _userRepository = userRepository;
            _notificationBusinessLogic = notificationBusinessLogic;
            _authService = authService;
        }

        public async Task<List<ComplexCardResponseDTO>> GetComplexesForAdminComplexIdAsync()
        { //El admin del complejo puede ver todos sus complejos (Active = true), en cualquier estado.
            var userId = _authService.GetUserId();
            //var userId = 1; Valor para probar
            List<Complex> complexes = await _complexRepository.GetComplexesByUserIdAsync(userId);
            List<ComplexCardResponseDTO> complexesCardsDTO = new List<ComplexCardResponseDTO>(); //= complexes.Select(ComplexMapper.toComplexCardResponseDTO).ToList();

            foreach (var complejo in complexes)
            {

                var lowestPricePerField = LowPriceForField(complejo);
                ComplexCardResponseDTO card = ComplexMapper.toComplexCardResponseDTO(complejo, lowestPricePerField);
                complexesCardsDTO.Add(card);
            }

            return complexesCardsDTO;
        }

        public async Task<List<ComplexSuperAdminResponseDTO>> GetAllComplexesBySuperAdminAsync()
        { //El  SuperAdmin puede ver todos, existentes y en cualquier estado
            //chequear que tenga rol superadmin
            var userRol = _authService.GetUserRole();

            if(userRol != "SuperAdmin")
                throw new UnauthorizedAccessException("No tenés permisos para ver todos los complejos.");

            var complexes = await _complexRepository.GetAllComplexesAsync();
            var complexesDTO = new List<ComplexSuperAdminResponseDTO>();
            foreach (var complex in complexes)
            {
                var userOwner = await _userRepository.GetUserByIdAsync(complex.UserId);
                complexesDTO.Add(ComplexMapper.toComplexSuperAdminResponseDTO(complex, userOwner.Name, userOwner.LastName));
            }
            return complexesDTO;
        }

        public async Task<List<ComplexSuperAdminResponseDTO>> GetLastFiveComplexesBySuperAdminAsync()
        { //El  SuperAdmin puede ver todos, existentes y en cualquier estado
            //chequear que tenga rol superadmin
            var userRol = _authService.GetUserRole();

            if (userRol != "SuperAdmin")
                throw new UnauthorizedAccessException("No tenés permisos para ver todos los complejos.");

            var complexes = await _complexRepository.GetLastFiveComplexAsync();
            var complexesDTO = new List<ComplexSuperAdminResponseDTO>();
            foreach (var complex in complexes)
            {
                var userOwner = await _userRepository.GetUserByIdAsync(complex.UserId);
                complexesDTO.Add(ComplexMapper.toComplexSuperAdminResponseDTO(complex, userOwner.Name, userOwner.LastName));
            }
            return complexesDTO;
        }

        public async Task<int> GetNumberOfComplexEnabled()
        {
            return await _complexRepository.GetNumberOfComplexEnabled();
        }

        public async Task<ComplexDetailResponseDTO> GetComplexByIdAsync(int complexId)
        { //El admin del complejo puede acceder a cualquier complejo que le pertenezca, en cualquier estado, el usuario solo a habilitados.

            var userId = _authService.GetUserIdOrNull();

            var userRol = userId.HasValue ?  _authService.GetUserRole() : null;
            
            var complex = await GetComplexBasicOrThrow(complexId);
            var complexDetailDTO = ComplexMapper.toComplexDetailResponseDTO(complex);
            complexDetailDTO.AverageRating = await _complexRepository.GetAverageRatingAsync(complexId);
            complexDetailDTO.UserId = complex.UserId;
            bool isOwner = userId.HasValue && complex.UserId == userId.Value;
            bool isSuperAdmin = userRol == "SuperAdmin";
            if (isOwner || isSuperAdmin)
                return complexDetailDTO;


            ValidateAccessForBasicUser(complex);
            return complexDetailDTO;
        }

        public async Task<ComplexDetailResponseDTO> CreateComplexAsync(CreateComplexRequestDTO createComplexDTO, string uploadPath)
        {

            var weekDays = createComplexDTO.TimeSlots.Select(ts => ts.WeekDay);
            var slots = createComplexDTO.TimeSlots;
            var userId = _authService.GetUserId();

            //checkear que el complejo pueda tener mismo horario de apertura y de cierre, eso singifica que ese dia esta cerrado

            if (weekDays.Distinct().Count() != 7)
            {
                throw new BadRequestException("No se pueden repetir dias de la semana en los horarios del complejo");
            }

            var earliestOpen = new TimeSpan(8, 0, 0);
            var latestClose = new TimeSpan(2, 0, 0);
            var latestCloseAdjusted = latestClose.Add(TimeSpan.FromDays(1)); // esto lo voy a usar para sumarle un dia a los horarios de cierre de complejo postoriores a las 12 de la noche

            foreach (var slot in slots)
            {
                var init = slot.InitTime.ToTimeSpan();
                var end = slot.EndTime.ToTimeSpan();

                // indentifico si cierra despues de medianoche
                bool closesNextDay = end < init;

                //aca corroboro si el horario de cierre es despues de medianoche, ajusto mi horario
                var endAdjusted = closesNextDay ? end.Add(TimeSpan.FromDays(1)) : end;

                if (init == end) continue; // ese dia esta cerrado


                if(init < earliestOpen)
                {
                    throw new BadRequestException($"El horario de apertura no puede ser anterior a las 8 de la mañana");
                }

                // valido que el horario de cierre no sea posterior a las 2 de la mañana
                if (endAdjusted > latestCloseAdjusted)
                {
                    throw new BadRequestException($"El horario de cierre no puede ser posterior a las 2 de la mañana");
                }
            }

            /*
            foreach (var slot in slots)
            {
                if (slot.EndTime < slot.InitTime)
                {
                    throw new BadRequestException($"El horario de fin debe ser mayor al horario de inicio para el día: {slot.WeekDay}");
                }
                if(slot.EndTime > // 2 de la mañana || slot.InitTme < 8 de la mañana)
                {
                    throw new BadRequestException($"El horario de apertura no debe ser anterior a las 8 de la mañana y el horario de cierre no debe ser posterior a las 2 de la mañana");
                }
            }
            */


            if (await _userBusinessLogic.GetUserByIdAsync(userId) == null)
            {
                throw new NotFoundException($"No se encontró el usuario con id {userId} asociado al complejo");
            }

            if (await _complexRepository.ExistsByNameAsync(createComplexDTO.Name))
            {
                throw new BadRequestException($"Ya existe un complejo con el nombre {createComplexDTO.Name}");
            }

            if (await _complexRepository.ExistsByAddressAsync(createComplexDTO.Street, createComplexDTO.Number, createComplexDTO.Locality, createComplexDTO.Province))
            {
                throw new BadRequestException($"Ya existe un complejo en la dirección {createComplexDTO.Street} {createComplexDTO.Number}, {createComplexDTO.Locality}, {createComplexDTO.Province}");
            }

            if (await _complexRepository.ExistsByPhoneAsync(createComplexDTO.Phone))
            {
                throw new BadRequestException($"Ya existe un complejo con el teléfono {createComplexDTO.Phone}");
            }

            var complex = ComplexMapper.toComplex(createComplexDTO,userId);

            if (createComplexDTO.ServicesIds.Count() > 0)
            {
                var services = await _serviceBusinessLogic.GetServicesByIdsAsync((List<int>)createComplexDTO.ServicesIds);
                complex.Services = services;
            }

            var imagePath = await ValidateAndSaveImage(createComplexDTO.Image, uploadPath);

            complex.ImagePath = imagePath;
            complex.Active = true;
            complex.State = ComplexState.Pendiente;
            await _complexRepository.CreateComplexAsync(complex);
            
            await _userBusinessLogic.UpdateUserRolAsync(complex.UserId, "AdminComplejo");


            //aca deberia crear la notificacion
            //necesito el id del SuperUser
            // SACO ESTO PARA PRUEBA
            //var superUserId = await _userBusinessLogic.GetUserIdByUserRolOrThrow("SuperAdmin");

            // PONGO ESTO PARA PROBAR QUE LES LLEGUEN LAS NOTIFICACIONES
            var superUserId = 7;

            var notification = new Notification
            {
                UserId = superUserId,
                Title = "Nuevo complejo pendiente de aprobación",
                Message = $"Administrador de complejo solicita habilitar el complejo '{createComplexDTO.Name}'.",
                ComplexId = complex.Id,
            };
            await _notificationBusinessLogic.CreateNotificationAsync(notification);

            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> UpdateComplexAsync(int complexId, UpdateComplexBasicInfoRequestDTO updateComplexDTO)
        {

            var complex = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            var userId = _authService.GetUserId();
            //var userId = 1;
            ValidateOwnerShip(complex, userId);
            ValidateEditable(complex);
            if (await _complexRepository.ExistsByNameAsync(updateComplexDTO.Name) && complex.Name != updateComplexDTO.Name)
            {
                throw new BadRequestException($"Ya existe un complejo con el nombre {updateComplexDTO.Name}");
            }

            if (await _complexRepository.ExistsByAddressAsync(updateComplexDTO.Street, updateComplexDTO.Number, updateComplexDTO.Locality, updateComplexDTO.Province)
                && (complex.Number != updateComplexDTO.Number || complex.Street != updateComplexDTO.Street || complex.Locality != updateComplexDTO.Locality || complex.Province != updateComplexDTO.Province))
            {
                throw new BadRequestException($"Ya existe un complejo en la dirección {updateComplexDTO.Street} {updateComplexDTO.Number}, {updateComplexDTO.Locality}, {updateComplexDTO.Province}");
            }

            if (await _complexRepository.ExistsByPhoneAsync(updateComplexDTO.Phone) && complex.Phone != updateComplexDTO.Phone)
            {
                throw new BadRequestException($"Ya existe un complejo con el teléfono {updateComplexDTO.Phone}");
            }

            complex.Name = updateComplexDTO.Name;
            complex.Description = updateComplexDTO.Description;
            complex.Province = updateComplexDTO.Province;
            complex.Locality = updateComplexDTO.Locality;
            complex.Street = updateComplexDTO.Street;
            complex.Number = updateComplexDTO.Number;
            complex.Phone = updateComplexDTO.Phone;
            complex.CBU = updateComplexDTO.CBU;
            complex.PercentageSign =    updateComplexDTO.PercentageSign;
            complex.AditionalIlumination = updateComplexDTO.AditionalIlumination;
            complex.StartIlumination = updateComplexDTO.StartIlumination;

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> UpdateTimeSlotsAsync(int complexId, UpdateTimeSlotComplexRequestDTO updateTimeSlotsDTO)
        {

            var complejo = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            var userId = _authService.GetUserId();
            //var userId = 1;
            ValidateOwnerShip(complejo, userId);
            ValidateEditable(complejo);

            var slotsDTO = updateTimeSlotsDTO.TimeSlots;

            if (slotsDTO.Select(s => s.WeekDay).Distinct().Count() != 7)
            {
                throw new BadRequestException($"No se pueden repetir dias de la semana en los horarios del complejo");
            }

            var earliestOpen = new TimeSpan(8, 0, 0);
            var latestClose = new TimeSpan(2, 0, 0);
            var latestCloseAdjusted = latestClose.Add(TimeSpan.FromDays(1));

            foreach (var slot in slotsDTO)
            {
                var init = slot.InitTime.ToTimeSpan();
                var end = slot.EndTime.ToTimeSpan();

                bool closesNextDay = end < init;

                var endAdjusted = closesNextDay ? end.Add(TimeSpan.FromDays(1)) : end;

                if (init == end) continue;


                if (init < earliestOpen)
                {
                    throw new BadRequestException($"El horario de apertura no puede ser anterior a las 8 de la mañana");
                }

                if (endAdjusted > latestCloseAdjusted)
                {
                    throw new BadRequestException($"El horario de cierre no puede ser posterior a las 2 de la mañana");
                }

                var existingSlot = complejo.TimeSlots
                    .FirstOrDefault(s => s.WeekDay == slot.WeekDay);

                existingSlot.InitTime = slot.InitTime;
                existingSlot.EndTime = slot.EndTime;
            }

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complejo);

        }

        public async Task<ComplexDetailResponseDTO> UpdateServicesAsync(int complexId, List<int> servicesIds)
        {
            var complex = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            var userId = _authService.GetUserId();
            //var userId = 1;
            ValidateOwnerShip(complex, userId);
            ValidateEditable(complex);
            var services = await _serviceBusinessLogic.GetServicesByIdsAsync(servicesIds);
            complex.Services = services;
            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> ChangeStateComplexAsync(int complexId, ComplexState newState)
        {//Hay transiciones de estado que puede hacer el superadmin y otras que puede hacer el admin del complejo.

            var complex = await GetComplexBasicOrThrow(complexId);

            if (complex.State == newState)
            {
                throw new BadRequestException($"El complejo ya se encuentra en este estado");
            }

            bool changed = false;
            // TRANSICIONES ADMIN DEL COMPLEJO 

            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();




            // bug silencioso, el admin tiene que ser el dueño del complejo
            //ValidateOwnerShip(complex, userId);

            Console.WriteLine("UserId: " + userId + ", Rol: " + userRol);

            if (userRol != "AdminComplejo" && userRol != "SuperAdmin")
            {
                throw new ForbiddenException($"No tiene los permisos para hacer esta operacion");
            }


            if (userRol == "AdminComplejo")
            {
                ValidateOwnerShip(complex, userId);
                if(complex.State == ComplexState.Habilitado && newState == ComplexState.Deshabilitado ||
                   complex.State == ComplexState.Deshabilitado && newState == ComplexState.Habilitado ||
                   complex.State == ComplexState.Rechazado && newState == ComplexState.Pendiente)
                {
                    complex.State = newState;
                    changed = true;
                }
            }
            else if(userRol == "SuperAdmin")
            {
                if(complex.State == ComplexState.Pendiente && (newState == ComplexState.Habilitado || newState == ComplexState.Rechazado))
                {
                    complex.State = newState;
                    changed = true;
                }
                else if ((complex.State == ComplexState.Habilitado || complex.State == ComplexState.Deshabilitado) && newState == ComplexState.Bloqueado)
                {
                    if (await _complexRepository.HasActiveReservationsInComplexAsync(complexId))
                    {
                        throw new BadRequestException("No se puede bloquear el complejo porque tiene reservas activas en sus canchas");
                    }
                    complex.State = newState;
                    changed = true;
                }else if (complex.State == ComplexState.Bloqueado && newState == ComplexState.Habilitado)
                {
                    complex.State = newState;
                    changed = true;
                }
            }
            else
            {
                throw new ForbiddenException("No tiene los permisos para hacer esta operación");
            }

            if (!changed)
                throw new BadRequestException($"Transición no permitida: no se puede cambiar de {complex.State} a {newState} con su rol actual");

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        // metodo que hace lo mismo que el de arriba pero refactorizado sin bugs
        public async Task<ComplexDetailResponseDTO> ChangeStateCompIexAsync( int complexId,ComplexState newState)
        {
            var complex = await GetComplexBasicOrThrow(complexId);

            if (complex.State == newState)
                throw new BadRequestException("El complejo ya se encuentra en este estado");

            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            // Validación de permisos base
            if (userRol == "AdminComplejo")
            {
                ValidateOwnerShip(complex, userId);
            }
            else if (userRol != "SuperAdmin")
            {
                throw new ForbiddenException("No tiene los permisos para hacer esta operación");
            }

            var previousState = complex.State;
            bool changed = false;

            
            // TRANSICIONES ADMIN COMPLEJO
            if (userRol == "AdminComplejo")
            {
                if (
                    (previousState == ComplexState.Habilitado && newState == ComplexState.Deshabilitado) ||
                    (previousState == ComplexState.Deshabilitado && newState == ComplexState.Habilitado) ||
                    (previousState == ComplexState.Rechazado && newState == ComplexState.Pendiente)
                )
                {
                    complex.State = newState;
                    changed = true;
                }
            }

            // TRANSICIONES SUPERADMIN
            if (userRol == "SuperAdmin")
            {
                // Pendiente → Habilitado / Rechazado
                if (
                    previousState == ComplexState.Pendiente &&
                    (newState == ComplexState.Habilitado || newState == ComplexState.Rechazado)
                )
                {
                    complex.State = newState;
                    changed = true;
                }

                // Habilitado / Deshabilitado → Bloqueado
                else if (
                    (previousState == ComplexState.Habilitado || previousState == ComplexState.Deshabilitado) &&
                    newState == ComplexState.Bloqueado
                )
                {
                    if (await _complexRepository.HasActiveReservationsInComplexAsync(complexId))
                    {
                        throw new BadRequestException(
                            "No se puede bloquear el complejo porque tiene reservas activas");
                    }

                    complex.State = newState;
                    changed = true;
                }

                // Bloqueado → Habilitado
                else if (
                    previousState == ComplexState.Bloqueado &&
                    newState == ComplexState.Habilitado
                )
                {
                    complex.State = newState;
                    changed = true;
                }
            }

            if (!changed)
                throw new BadRequestException(
                    $"No se puede cambiar el estado de {previousState} a {newState}");

            // NOTIFICACIÓN (POST CAMBIO)
            if (
                userRol == "SuperAdmin" &&
                previousState == ComplexState.Pendiente &&
                (newState == ComplexState.Habilitado || newState == ComplexState.Rechazado)
            )
            {
                var notification = new Notification
                {
                    UserId = complex.UserId,
                    Title = newState == ComplexState.Habilitado
                        ? "Tu complejo fue aprobado"
                        : "Tu complejo fue rechazado",
                    Message = newState == ComplexState.Habilitado
                        ? $"Tu complejo '{complex.Name}' fue aprobado."
                        : $"Tu complejo '{complex.Name}' fue rechazado.",
                    ComplexId = complex.Id,
                    Context = NotificationContext.ADMIN_COMPLEX_ACCTION
                };

                await _notificationBusinessLogic.CreateNotificationAsync(notification);
            }

            await _complexRepository.SaveAsync();

            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }



        public async Task<List<ComplexCardResponseDTO>> SearchAvailableComplexes(ComplexFiltersRequestDTO complexFiltersDTO)
        {
            // Convierto la fecha a mi enum
            var weekDay = ConvertToWeekDay(complexFiltersDTO.Date);


            // Me traigo los complejos ya filtrados por localidad, si esta activo y si esta habilitado, incluyendo timeSlot, canchas, reservas y reservas recurrentes
            var complexes = await _complexRepository.GetComplexesWithFiltersAsync(complexFiltersDTO.Province, complexFiltersDTO.Locality, complexFiltersDTO.FieldType);

            // Aca es donde voy a ir metiendo los complejos que voy a devolver
            List<ComplexCardResponseDTO> result = new();

            foreach (var c in complexes)
            {
                // Valido que el complejo esté abierto ese día/hora
                bool complexOpen = c.TimeSlots.Any(ts =>
                {
                    if (ts.WeekDay != weekDay) return false;

                    if (ts.EndTime > ts.InitTime)
                    {
                        return complexFiltersDTO.Hour >= ts.InitTime && complexFiltersDTO.Hour < ts.EndTime;
                    }
                    else
                    {
                        return complexFiltersDTO.Hour >= ts.InitTime || complexFiltersDTO.Hour < ts.EndTime;
                    }
                });

                if (!complexOpen)
                    continue;

                // Veo si hay canchas disponibles
                bool hasAvailableField = c.Fields.Any(field =>
                {
                    if (field.FieldType != complexFiltersDTO.FieldType)
                        return false;
                    // Checkeo que no tenga reservas
                    bool hasReservation = field.Reservations.Any(r =>
                        r.Date == complexFiltersDTO.Date &&
                        r.InitTime == complexFiltersDTO.Hour &&
                        (r.ReservationState == ReservationState.Aprobada || r.ReservationState == ReservationState.Pendiente)
                    );

                    if (hasReservation)
                        return false;

                    // Checkeo que no tenga un bloqueo reccurrente en ese dia y hora
                    bool isBlocked = field.RecurringCourtBlocks.Any(b =>
                        b.WeekDay == weekDay &&
                        b.InitHour <= complexFiltersDTO.Hour &&
                        b.EndHour > complexFiltersDTO.Hour
                    );

                    if (isBlocked)
                        return false;

                    // Si pasa todo la cancha está disponible
                    return true;
                });

                if (hasAvailableField)
                {
                    var lowestPrice = LowPriceForField(c);
                    // La agrego a la lista de complejos a devolver
                    result.Add(ComplexMapper.toComplexCardResponseDTO(c, lowestPrice));
                }
            }
            return result;
        }

        public async Task DeleteComplexAsync(int complexId)
        {
            var complex = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            var userId = _authService.GetUserId();
            //var userId = 1;
            ValidateOwnerShip(complex, userId);
            if (await _complexRepository.HasActiveReservationsInComplexAsync(complexId))
            {
                throw new BadRequestException("No se puede eliminar el complejo porque tiene reservas activas en sus canchas");
            }
            complex.Active = false;
            foreach (var field in complex.Fields)
            {
                field.Active = false;
            }
            await _complexRepository.SaveAsync();
        }

        // Funcion auxiliar para asegurarme que se matchee bien DayOfWeek con nuesto enum y para no modificar el enum
        public WeekDay ConvertToWeekDay(DateOnly date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Monday => WeekDay.Lunes,
                DayOfWeek.Tuesday => WeekDay.Martes,
                DayOfWeek.Wednesday => WeekDay.Miercoles,
                DayOfWeek.Thursday => WeekDay.Jueves,
                DayOfWeek.Friday => WeekDay.Viernes,
                DayOfWeek.Saturday => WeekDay.Sabado,
                DayOfWeek.Sunday => WeekDay.Domingo,
                _ => throw new BadRequestException("Día inválido")
            };
        }
        public async Task<Complex?> GetComplexBasicOrThrow(int complexId)
        {
            var complex = await _complexRepository.GetComplexByIdWithBasicInfoAsync(complexId);
            if (complex == null) throw new NotFoundException($"El complejo con id {complexId} no existe");
            return complex;
        }

        public async Task<Complex?> GetComplexWithFieldsOrThrow(int complexId)
        {
            var complex = await _complexRepository.GetComplexByIdWithFieldsAsync(complexId);
            if (complex == null) throw new NotFoundException($"El complejo con id {complexId} no existe");
            return complex;
        }

        public void ValidateEditable(Complex complex)
        {
            if (complex.State == ComplexState.Bloqueado || complex.State == ComplexState.Pendiente)
            {
                throw new BadRequestException("El complejo no puede ser editado mientras está en estado pendiente o bloqueado");
            }
        }

        public void ValidateOwnerShip(Complex complex, int userId)
        {
            if (complex.UserId != userId)
            {
                throw new BadRequestException("No tiene permisos para manipular este complejo");
            }
        }

        public void ValidateAccessForBasicUser(Complex complex)
        {
            if (complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no se encuentra habilitado");
            }
        }

        private async Task<string> ValidateAndSaveImage(IFormFile image, string uploadPath)
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
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await image.CopyToAsync(stream);

            return $"/uploads/complexes/{fileName}";
        }

        public void ValidateFieldOperationsAllowed(Domain.Entities.Complex complex)
        {
            if (complex.State == ComplexState.Pendiente || complex.State == ComplexState.Rechazado || complex.State == ComplexState.Bloqueado)
            {
                throw new BadRequestException("No se pueden manipular las canchas del complejo en el estado actual");
            }
        }

        /*
        public async Task ApproveComplexAsync(AproveComplexRequestDTO request)
        {
            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            var complex = await _complexRepository.GetComplexByIdWithBasicInfoAsync(request.ComplexId);


            // Validaciones
            ValidateComplexApproval(complex, userRol);

            complex.State = ComplexState.Habilitado;
            await _complexRepository.SaveAsync();

            // creo la notificacion 
            var notification = new Notification
            {
                UserId = complex.UserId,
                Title = "Tu complejo fue aprobado",
                Message = $"Tu complejo '{complex.Name}' fue aprobado.",
                ComplexId = complex.Id,
            };

            await _notificationBusinessLogic.CreateNotificationAsync(notification);
        }

        public async Task RejectComplexAsync(RejectComplexRequestDTO request)
        {
            var userId = _authService.GetUserId();
            var userRol = _authService.GetUserRole();

            var complex = await _complexRepository.GetComplexByIdWithBasicInfoAsync(request.ComplexId);

            ValidateComplexApproval(complex, userRol);

            complex.State = ComplexState.Rechazado;
            await _complexRepository.SaveAsync();

            var notification = new Notification
            {
                UserId = complex.UserId,
                Title = "Tu complejo fue rechazado",
                Message = $"El complejo fue rechazado. Motivo: {request.Reason}.",
                ComplexId = complex.Id,
            };

            await _notificationBusinessLogic.CreateNotificationAsync(notification);
        }
        */

        private void ValidateComplexApproval(Complex complex, string userRol)
        {
            // debe existir
            if (complex == null)
                throw new BadRequestException("El complejo no existe");

            // debe estar pendiente
            if (complex.State != ComplexState.Pendiente)
                throw new BadRequestException("Solo se pueden aprobar complejos pendientes");

            // debe ser admin del complejo o superuser
            if (userRol != "SuperAdmin")
            {
                throw new BadRequestException("No tiene permisos para aprobar complejos");
            }
            
        }

        private decimal LowPriceForField(Complex complex)
        {
            if (complex.Fields == null || !complex.Fields.Any())
            {
                return 0;
            }
            return complex.Fields.Min(f => f.HourPrice);
        }

        public async Task<ComplexOwnerDTO> GetComplexOwnerAsync(int idComplex)
        {
            var complex = await GetComplexBasicOrThrow(idComplex);
            if (complex == null)
            {
                throw new NotFoundException($"No existe ningún complejo con id: {idComplex}");
            }

            var owner = await _userRepository.GetUserByIdAsync(complex.UserId);
            if (owner == null)
            {
                throw new NotFoundException($"No existe ningún administrador de complejo con id: {complex.UserId}");
            }
            return (new ComplexOwnerDTO
            {
                Name = owner.Name,
                LastName = owner.LastName,
            });
        }

        public async Task<List<Complex>>? GetComplexesByOwnerIdAsync(int idUser)
        {
            var complexes = await _complexRepository.GetComplexesByUserIdAsync(idUser);
           
            return complexes;
        }

    }
}
