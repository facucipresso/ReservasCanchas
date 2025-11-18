using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ComplexBusinessLogic
    {
        private readonly ComplexRepository _complexRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public ComplexBusinessLogic(ComplexRepository complexRepository, ServiceRepository serviceRepository, UsuarioRepository usuarioRepository)
        {
            _complexRepository = complexRepository;
            _serviceRepository = serviceRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<ComplexCardResponseDTO>> GetComplexesForAdminComplexIdAsync(int adminComplexId)
        { //El admin del complejo puede ver todos sus complejos (Active = true), en cualquier estado.
            //chequear que tenga rol admincomplex
            List<Complex> complexes = await _complexRepository.GetComplexesByUserIdAsync(adminComplexId);
            var complexescardsdto = complexes.Select(ComplexMapper.toComplexCardResponseDTO).ToList();
            return complexescardsdto;
        }

        public async Task<List<ComplexSuperAdminResponseDTO>> GetAllComplexesBySuperAdminAsync()
        { //El  SuperAdmin puede ver todos, existentes o eliminados y en cualquier estado
            //chequear que tenga rol superadmin
            var complexes = await _complexRepository.GetAllComplexesAsync();
            return complexes.Select(ComplexMapper.toComplexSuperAdminResponseDTO).ToList();
        }

        public async Task<ComplexDetailResponseDTO> GetComplexByIdForAdminComplexAsync(int id)
        { //Solo trae activos, cuando un Admin de complejo accede a un complejo especifico de los suyos (En cualquier estado)
            //Debemos obtener el adminid y chequear si coincide con el usuario admin del complejo
            var complex = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            //Chequeamos owner
            //var adminComplexId = _authService.GetUserIdFromToken();
            var complexAdminId = 1;
            EnsureComplexExists(complex, id);
            EnsureOwner(complex, complexAdminId);
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> GetComplexByIdAsync(int id)
        { //Este solo trae activos, cuando un usuario selecciona un complejo para acceder a su detalle. (Solo HABILITADOS)
            var complex = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            EnsureComplexExists(complex, id);
            EnsureComplexEditable(complex);
            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> CreateComplexAsync(CreateComplexRequestDTO complexDTO)
        {
            var complex = ComplexMapper.toComplex(complexDTO);

            if (complexDTO.ServicesIds.Count() > 0)
            {
                var services = await _serviceRepository.GetServicesByIdsAsync((List<int>)complexDTO.ServicesIds);
                complex.Services = services;
            }

            var weekDays = complexDTO.TimeSlots.Select(ts => ts.WeekDay);
            if (weekDays.Distinct().Count() != weekDays.Count())
            {
                throw new BadRequestException("No se pueden repetir dias de la semana en los horarios del complejo");
            }

            if (await _usuarioRepository.GetUserByIdAsync(complexDTO.UserId) == null)
            {
                throw new NotFoundException($"No se encontró el usuario con id {complexDTO.UserId} asociado al complejo");
            }

            if (await _complexRepository.ExistsByNameAsync(complexDTO.Name))
            {
                throw new BadRequestException($"Ya existe un complejo con el nombre {complexDTO.Name}");
            }

            if (await _complexRepository.ExistsByAddressAsync(complexDTO.Street, complexDTO.Number, complexDTO.Locality, complexDTO.Province))
            {
                throw new BadRequestException($"Ya existe un complejo en la dirección {complexDTO.Street} {complexDTO.Number}, {complexDTO.Locality}, {complexDTO.Province}");
            }

            if (await _complexRepository.ExistsByPhoneAsync(complexDTO.Phone))
            {
                throw new BadRequestException($"Ya existe un complejo con el teléfono {complexDTO.Phone}");
            }

            complex.Active = true;
            complex.State = ComplexState.Pendiente;
            await _complexRepository.CreateComplexAsync(complex);

            return ComplexMapper.toComplexDetailResponseDTO(complex);
        }

        public async Task<ComplexDetailResponseDTO> UpdateComplexAsync(int id, UpdateComplexBasicInfoRequestDTO complexDTO)
        {
            // FALTARIA LA VALIDACION DE QUE EL ID DEL USUARIO QUE QUIERE EDITAR SEA EL MISMO ID QUE ESTA
            // EN LA COMPLEJO COMO UsuarioId, PERO ESO LO SACAMOS DEL TOKEN

            var complejo = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            //Chequeamos owner
            //var adminComplexId = _authService.GetUserIdFromToken();
            var complexAdminId = 1;
            EnsureComplexExists(complejo, id);
            EnsureOwner(complejo, complexAdminId);
            EnsureComplexEditable(complejo);
            // Verificar que estas tres validaciones estan correctas
            if (await _complexRepository.ExistsByNameAsync(complexDTO.Name) && complejo.Name != complexDTO.Name)
            {
                throw new BadRequestException($"Ya existe un complejo con el nombre {complexDTO.Name}");
            }

            if (await _complexRepository.ExistsByAddressAsync(complexDTO.Street, complexDTO.Number, complexDTO.Locality, complexDTO.Province) && (complejo.Number != complexDTO.Number && complejo.Street != complexDTO.Street && complejo.Locality != complexDTO.Locality && complejo.Province != complexDTO.Province))
            {
                throw new BadRequestException($"Ya existe un complejo en la dirección {complexDTO.Street} {complexDTO.Number}, {complexDTO.Locality}, {complexDTO.Province}");
            }

            if (await _complexRepository.ExistsByPhoneAsync(complexDTO.Phone) && complejo.Phone != complexDTO.Phone)
            {
                throw new BadRequestException($"Ya existe un complejo con el teléfono {complexDTO.Phone}");
            }

            complejo.Name = complexDTO.Name;
            complejo.Description = complexDTO.Description;
            complejo.Province = complexDTO.Province;
            complejo.Locality = complexDTO.Locality;
            complejo.Street = complexDTO.Street;
            complejo.Number = complexDTO.Number;
            complejo.Phone = complexDTO.Phone;

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complejo);
        }

        public async Task<ComplexDetailResponseDTO> UpdateTimeSlotsAsync(int id, UpdateTimeSlotComplexRequestDTO updateTimeSlotComplexRequestDTO)
        {
            // FALTARIA LA VALIDACION DE QUE EL ID DEL USUARIO QUE QUIERE EDITAR SEA EL MISMO ID QUE ESTA
            // EN LA COMPLEJO COMO UsuarioId, PERO ESO LO SACAMOS DEL TOKEN

            var complejo = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            //Chequeamos owner
            //var adminComplexId = _authService.GetUserIdFromToken();
            var complexAdminId = 1;
            EnsureComplexExists(complejo, id);
            EnsureOwner(complejo, complexAdminId);
            EnsureComplexEditable(complejo);

            var slots = updateTimeSlotComplexRequestDTO.TimeSlots;

            if (slots.Select(s => s.WeekDay).Distinct().Count() != slots.Count())
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

        public async Task<ComplexDetailResponseDTO> UpdateServicesAsync(int id, List<int> servicesIds)
        {
            // FALTARIA LA VALIDACION DE QUE EL ID DEL USUARIO QUE QUIERE EDITAR SEA EL MISMO ID QUE ESTA
            // EN LA COMPLEJO COMO UsuarioId, PERO ESO LO SACAMOS DEL TOKEN
            var complejo = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            //Chequeamos owner
            //var adminComplexId = _authService.GetUserIdFromToken();
            var complexAdminId = 1;
            EnsureComplexExists(complejo, id);
            EnsureOwner(complejo, complexAdminId);
            EnsureComplexEditable(complejo);
            var services = await _serviceRepository.GetServicesByIdsAsync(servicesIds);
            complejo.Services = services;
            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complejo);
        }

        public async Task<ComplexDetailResponseDTO> ChangeStateComplexAsync(int id, ComplexState newState)
        {
            //Con chequear rol ya funcionaria.
            /*var user = await _usuarioRepository.GetUserByIdAsync(superUserId);
            if(user.Rol != Rol.SuperAdmin)
            {
                throw new BadRequestException($"No tiene los permisos para hacer esta operacion");
            }*/

            var complejo = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            EnsureComplexExists(complejo, id);

            if (complejo.State == newState)
            {
                throw new BadRequestException($"El complejo ya se encuentra en este estado");
            }

            if (complejo.State == ComplexState.Pendiente && (newState == ComplexState.Habilitado || newState == ComplexState.Bloqueado))
            {
                complejo.State = newState;
                // TODO OK
            }
            else if (complejo.State == ComplexState.Habilitado && newState == ComplexState.Bloqueado)
            {
                complejo.State = newState;
                // TODO OK
            }
            else if (complejo.State == ComplexState.Bloqueado && newState == ComplexState.Habilitado)
            {
                complejo.State = newState;
            }
            else
            {
                // Debería ser Forbidden (403) que el usuario no tiene permisos
                throw new BadRequestException($"No se puede pasar a ese estado del complejo");
            }

            await _complexRepository.SaveAsync();
            return ComplexMapper.toComplexDetailResponseDTO(complejo);
        }

        public async Task<List<ComplexCardResponseDTO>> SearchAvailableComplexes(ComplexSearchRequestDTO request)
        {
            // Convierto la fecha a mi enum
            var weekDay = ConvertToWeekDay(request.Date);


            // Me traigo los complejos ya filtrados por localidad, si esta activo y si esta habilitado, incluyendo timeSlot, canchas, reservas y reservas recurrentes
            var complexes = await _complexRepository.GetComplexesForSearchAsync(request.Province, request.Locality, request.FieldType);

            // Aca es donde voy a ir metiendo los complejos que voy a devolver
            List<ComplexCardResponseDTO> result = new();

            foreach (var c in complexes)
            {
                // Valido que el complejo esté abierto ese día/hora
                bool complexOpen = c.TimeSlots.Any(ts =>
                    ts.WeekDay == weekDay &&
                    ts.InitTime <= request.Hour &&
                    ts.EndTime > request.Hour
                );

                if (!complexOpen)
                    continue;

                // Veo si hay canchas disponibles
                bool hasAvailableField = c.Fields.Any(field =>
                {
                    // Checkeo que no tenga reservas
                    bool hasReservation = field.Reservations.Any(r =>
                        r.Date == request.Date &&
                        r.InitTime == request.Hour &&
                        r.ReservationState != ReservationState.CanceladoConDevolucion &&
                        r.ReservationState != ReservationState.CanceladoSinDevolucion &&
                        r.ReservationState != ReservationState.Pendiente
                    );

                    if (hasReservation)
                        return false;

                    // Checkeo que no tenga un bloqueo reccurrente en ese dia y hora
                    bool isBlocked = field.RecurringCourtBlocks.Any(b =>
                        b.WeekDay == weekDay &&
                        b.InitHour <= request.Hour &&
                        b.EndHour > request.Hour
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

        public async Task DeleteComplexAsync(int id)
        {
            //QUE HACER SI EL COMPLEJO TIENE CANCHAS CON RESERVAS SIN COMPLETAR?
            var complejo = await _complexRepository.GetComplexByIdWithRelationsAsync(id);
            //Chequeamos owner
            //var adminComplexId = _authService.GetUserIdFromToken();
            var complexAdminId = 1;
            EnsureComplexExists(complejo, id);
            EnsureOwner(complejo, complexAdminId);
            complejo.Active = false;
            foreach (var field in complejo.Fields)
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

        private void EnsureOwner(Complex complex, int adminComplexId)
        {
            if (complex.UserId != adminComplexId)
            {
                throw new BadRequestException($"No se pueden actualizar complejos ajenos");
            }
        }

        private void EnsureComplexEditable(Complex complex)
        {
            if (complex.State == ComplexState.Pendiente || complex.State == ComplexState.Bloqueado)
            {
                throw new BadRequestException($"No se pueden actualizar datos de un complejo que no se encuentra habilitado");
            }
        }

        private void EnsureComplexExists(Complex complex, int id)
        {
            if (complex == null)
            {
                throw new BadRequestException($"No existe un complejo con el id {id}");
            }
        }

        public void ComplexValidityStateCheck(Complex complex)
        {
            EnsureComplexEditable(complex);
        }

        public async Task<Complex> ComplexValidityExistenceCheck(int complexId)
        {
            var complex =  await _complexRepository.GetComplexByIdAsync(complexId);
            EnsureComplexExists(complex, complexId);
            return complex;
        }

        public async Task<Complex> ComplexValidityExistenceCheck2(int complexId)
        {
            var complex = await _complexRepository.GetComplexByIdWithReservationsAsync(complexId);
            EnsureComplexExists(complex, complexId);
            return complex;
        }

        public void ComplexValidityAdmin(Complex complex, int adminComplexId)
        {
            EnsureOwner(complex, adminComplexId);
        }


    }
}
