using Microsoft.AspNetCore.Http;
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

namespace ReservasCanchas.BusinessLogic
{
    public class ComplexBusinessLogic
    {
        private readonly ComplexRepository _complexRepository;
        private readonly ServiceBusinessLogic _serviceBusinessLogic;
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly NotificationBusinessLogic _notificationBusinessLogic;

        public ComplexBusinessLogic(ComplexRepository complexRepository, ServiceBusinessLogic serviceBusinessLogic, UserBusinessLogic usuarioBusinessLogic, NotificationBusinessLogic notificationBusinessLogic)
        {
            _complexRepository = complexRepository;
            _serviceBusinessLogic = serviceBusinessLogic;
            _userBusinessLogic = usuarioBusinessLogic;
            _notificationBusinessLogic = notificationBusinessLogic;
        }

        public async Task<List<ComplexCardResponseDTO>> GetComplexesForAdminComplexIdAsync()
        { //El admin del complejo puede ver todos sus complejos (Active = true), en cualquier estado.
            //var userId = _authService.GetUserIdFromToken();
            var userId = 1; //Valor para probar
            List<Complex> complexes = await _complexRepository.GetComplexesByUserIdAsync(userId);
            var complexesCardsDTO = complexes.Select(ComplexMapper.toComplexCardResponseDTO).ToList();
            return complexesCardsDTO;
        }

        public async Task<List<ComplexSuperAdminResponseDTO>> GetAllComplexesBySuperAdminAsync()
        { //El  SuperAdmin puede ver todos, existentes y en cualquier estado
            //chequear que tenga rol superadmin
            //var userRol = _authService.GetUserRoleFromToken();
            //if(userRol != Rol.SuperAdmin){Exception Unhautorized}
            var complexes = await _complexRepository.GetAllComplexesAsync();
            return complexes.Select(ComplexMapper.toComplexSuperAdminResponseDTO).ToList();
        }
        public async Task<ComplexDetailResponseDTO> GetComplexByIdAsync(int complexId)
        { //El admin del complejo puede acceder a cualquier complejo que le pertenezca, en cualquier estado, el usuario solo a habilitados.
            //var userId = _authService.GetUserIdFromToken();
            //var userRol = _authService.GetUserRoleFromToken();
            int userId = 1; //Valor para probar
            var complex = await GetComplexBasicOrThrow(complexId);


            if (complex.UserId == userId) // || rol == Rol.SuperAdmin
                return ComplexMapper.toComplexDetailResponseDTO(complex);

            ValidateAccessForBasicUser(complex);
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> CreateComplexAsync(CreateComplexRequestDTO createComplexDTO, string uploadPath)
        {

            // -----------------------------------------------------------
            // 1. DESERIALIZAR LAS FRANJAS HORARIAS QUE VIENEN COMO STRING
            // -----------------------------------------------------------
            // El frontend envía TimeSlots como string JSON porque es multipart/form-data.
            // Acá lo convertimos en una lista real de TimeSlotComplexRequestDTO.
            /* esto es despues de los cambios
            var timeSlots = JsonConvert.DeserializeObject<List<TimeSlotComplexRequestDTO>>(createComplexDTO.TimeSlots);

            if (timeSlots == null || timeSlots.Count != 7)
            {
                throw new BadRequestException("Se debe especificar una franja horaria por cada día de la semana");
            }

            // Guardamos las franjas ya convertidas en el DTO para que el mapper pueda usarlas
            createComplexDTO.TimeSlotsList = timeSlots;*/

            //esto por ahora no lo llamo
            //var complex = ComplexMapper.toComplex(createComplexDTO);

            /* esto es despues de los cambios
            var weekDays = timeSlots.Select(ts => ts.WeekDay);
            if (weekDays.Distinct().Count() != 7)
            {
                throw new BadRequestException("No se pueden repetir días de la semana en los horarios del complejo");
            }*/

            /* LO AGREGUE MAS ABAJO antes de los cambios
            if (createComplexDTO.ServicesIds.Count() > 0)
            {
                var services = await _serviceBusinessLogic.GetServicesByIdsAsync((List<int>)createComplexDTO.ServicesIds);
                complex.Services = services;
            }
            */

            //esto es antes de los cambios
            var weekDays = createComplexDTO.TimeSlots.Select(ts => ts.WeekDay);
            if (weekDays.Distinct().Count() != 7)
            {
                throw new BadRequestException("No se pueden repetir dias de la semana en los horarios del complejo");
            }
            

            if (await _userBusinessLogic.GetUserByIdAsync(createComplexDTO.UserId) == null)
            {
                throw new NotFoundException($"No se encontró el usuario con id {createComplexDTO.UserId} asociado al complejo");
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

            var complex = ComplexMapper.toComplex(createComplexDTO);

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
            
            await _userBusinessLogic.UpdateUserRolAsync(complex.UserId, Rol.AdminComplejo);


            //aca deberia crear la notificacion
            //necesito el id del SuperUser
            var superUserId = await _userBusinessLogic.GetUserIdByUserRolOrThrow(Rol.SuperAdmin);

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
            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
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

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> UpdateTimeSlotsAsync(int complexId, UpdateTimeSlotComplexRequestDTO updateTimeSlotsDTO)
        {

            var complejo = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
            ValidateOwnerShip(complejo, userId);
            ValidateEditable(complejo);

            var slots = updateTimeSlotsDTO.TimeSlots;

            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
            {
                throw new BadRequestException($"No se pueden repetir dias de la semana en los horarios del complejo");
            }

            //Falta chequear si los time slots no tienen reservas asociadas antes de permitir la actualizacion
            //Tenemos q traer las reservas aprobadas pero no completadas y ver si tienen alguna en los dias y horarios que se quieren modificar
            //Y si la modificacion los dejaria fuera del rango.
            foreach (var slot in slots)
            {
                if (slot.EndTime <= slot.InitTime)
                {
                    throw new BadRequestException($"El horario de fin debe ser mayor al horario de inicio para el día {slot.WeekDay}");
                }
                var existing = complejo.TimeSlots.First(ts => ts.WeekDay == slot.WeekDay);
                existing.InitTime = slot.InitTime;
                existing.EndTime = slot.EndTime;
            }

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complejo);

        }

        public async Task<ComplexDetailResponseDTO> UpdateServicesAsync(int complexId, List<int> servicesIds)
        {
            var complex = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
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
            // ***** TRANSICIONES ADMIN DEL COMPLEJO ***** //

            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
            ValidateOwnerShip(complex, userId);

            if (complex.State == ComplexState.Habilitado && newState == ComplexState.Deshabilitado)
            {
                complex.State = newState;
                changed = true;
            }


            if (complex.State == ComplexState.Deshabilitado && newState == ComplexState.Habilitado)
            {
                complex.State = newState;
                changed = true;
            }

            if (complex.State == ComplexState.Rechazado && newState == ComplexState.Pendiente)
            {
                complex.State = newState;
                changed = true;
            }

            // ***** TRANSICIONES SUPERADMIN ***** //

            //var userRol = _authService.GetUserRoleFromToken();
            var userRol = Rol.SuperAdmin;
            if (userRol != Rol.SuperAdmin)
            {
                throw new BadRequestException($"No tiene los permisos para hacer esta operacion");
            }

            if (complex.State == ComplexState.Pendiente && (newState == ComplexState.Habilitado || newState == ComplexState.Rechazado))
            {
                complex.State = newState;
                changed = true;
            }

            if ((complex.State == ComplexState.Habilitado || complex.State == ComplexState.Deshabilitado) && newState == ComplexState.Bloqueado)
            {
                if (await _complexRepository.HasActiveReservationsInComplexAsync(complexId))
                {
                    throw new BadRequestException("No se puede bloquear el complejo porque tiene reservas activas en sus canchas");
                }
                complex.State = newState;
                changed = true;
            }

            if (complex.State == ComplexState.Bloqueado && newState == ComplexState.Habilitado)
            {
                complex.State = newState;
                changed = true;
            }

            if (!changed)
                throw new BadRequestException($"No se puede cambiar el estado de {complex.State} a {newState}");

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
                    ts.WeekDay == weekDay &&
                    ts.InitTime <= complexFiltersDTO.Hour &&
                    ts.EndTime > complexFiltersDTO.Hour
                );

                if (!complexOpen)
                    continue;

                // Veo si hay canchas disponibles
                bool hasAvailableField = c.Fields.Any(field =>
                {
                    // Checkeo que no tenga reservas
                    bool hasReservation = field.Reservations.Any(r =>
                        r.Date == complexFiltersDTO.Date &&
                        r.InitTime == complexFiltersDTO.Hour &&
                        r.ReservationState == ReservationState.Aprobada
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
                    // La agrego a la lista de complejos a devolver
                    result.Add(ComplexMapper.toComplexCardResponseDTO(c));
                }
            }
            return result;
        }

        public async Task DeleteComplexAsync(int complexId)
        {
            var complex = await GetComplexBasicOrThrow(complexId);
            //Chequeamos owner
            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
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
                throw new BadRequestException("El complejo no se encuentra habilitado para edición");
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
                throw new BadRequestException("No se pueden agregar canchas al complejo en el estado actual");
            }
        }

        public async Task ApproveComplexAsync(AproveComplexRequestDTO request)
        {
            var userId = 1; //_authService.getUserId();
            var userRol = Rol.SuperAdmin; //_authService.getRol();

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
            var userId = 1; //_authService.getUserId();
            var userRol = Rol.AdminComplejo; //_authService.getRol();

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

        private void ValidateComplexApproval(Complex complex, Rol userRol)
        {
            // debe existir
            if (complex == null)
                throw new BadRequestException("El complejo no existe");

            // debe estar pendiente
            if (complex.State != ComplexState.Pendiente)
                throw new BadRequestException("Solo se pueden aprobar complejos pendientes");

            // debe ser admin del complejo o superuser
            if (userRol != Rol.SuperAdmin)
            {
                throw new BadRequestException("No tiene permisos para aprobar complejos");
            }
            
        }
    }
}
