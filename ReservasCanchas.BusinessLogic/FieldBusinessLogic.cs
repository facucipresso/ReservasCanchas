using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System.Data;

namespace ReservasCanchas.BusinessLogic
{
    public class FieldBusinessLogic
    {
        private readonly FieldRepository _fieldRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly AuthService _authService;
        public FieldBusinessLogic(FieldRepository fieldRepository, ComplexBusinessLogic complexBusinessLogic, AuthService authService, ReservationRepository reservationRepository )
        {
            _fieldRepository = fieldRepository;
            _complexBusinessLogic = complexBusinessLogic;
            _authService = authService;
            _reservationRepository = reservationRepository;
        }
        public async Task<FieldDetailResponseDTO> GetFieldByIdAsync(int fieldId)
        {
            var userId = _authService.GetUserId();
            var field = await GetFieldWithRelationsOrThrow(fieldId);
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);

            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllFieldsByComplexIdAsync(int complexId)
        {
            
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var userId = _authService.GetUserIdOrNull();
            var fields = await _fieldRepository.GetAllFieldsByComplexIdWithRelationsAsync(complexId);
            if (userId == null || complex.UserId != userId)
            {
                return fields
                     .Where(f => f.FieldState == FieldState.Habilitada)
                     .Select(FieldMapper.ToFieldDetailResponseDTO)
                     .ToList();
            }
            var userRol = _authService.GetUserRole();
            
            var fieldsResponse = fields
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsResponse;
        }

        public async Task<FieldDetailResponseDTO> CreateFieldAsync(CreateFieldRequestDTO createFieldDTO)
        {
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(createFieldDTO.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            var slotsComplex = complex.TimeSlots;

            var slots = createFieldDTO.TimeSlotsField;


            if (slots.Count != 7)
                throw new BadRequestException("Debes enviar exactamente 7 franjas horarias (una por día)");

            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
                throw new BadRequestException("Los días de la semana no pueden repetirse");

            foreach (var fieldSlot in slots)
            {

                var day = fieldSlot.WeekDay;

                var complexSlot = complex.TimeSlots.FirstOrDefault(s => s.WeekDay == day);


                if (complexSlot == null)

                {
                    throw new BadRequestException(
                        $"El complejo no tiene horario configurado para el día {day}"
                    );
                }

                var cInit = complexSlot.StartTime.ToTimeSpan();
                var cEnd = complexSlot.EndTime.ToTimeSpan();
                var fInit = fieldSlot.StartTime.ToTimeSpan();
                var fEnd = fieldSlot.EndTime.ToTimeSpan();

                // cerrado = init == end
                bool complexClosed = cInit == cEnd;
                bool fieldClosed = fInit == fEnd;

                // si el complejo está cerrado, la cancha también debe estarlo
                if (complexClosed)
                {
                    if (!fieldClosed)
                    {
                        throw new BadRequestException(
                            $"La cancha debe estar cerrada el día {day} porque el complejo está cerrado"
                        );
                    }
                    continue;
                }

                // si la cancha está cerrada y el complejo abierto, OK
                if (fieldClosed)
                    continue;

                //  cierre post medianoche
                if (cEnd <= cInit)
                    cEnd = cEnd.Add(TimeSpan.FromDays(1));

                if (fEnd <= fInit)
                    fEnd = fEnd.Add(TimeSpan.FromDays(1));

                // validaciones de rango
                if (fInit < cInit)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede abrir antes que el complejo ({fieldSlot.StartTime} < {complexSlot.StartTime})"
                    );
                }

                if (fEnd > cEnd)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede cerrar después que el complejo ({fieldSlot.EndTime} > {complexSlot.EndTime})"
                    );
                }

            }

            var field = FieldMapper.ToField(createFieldDTO);
            field.Active = true;
            field.FieldState = FieldState.Habilitada;
            await _fieldRepository.CreateFieldAsync(field);

            field.RecurringCourtBlocks ??= new List<RecurringFieldBlock>();

            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> UpdateFieldAsync(int fieldId, UpdateFieldRequestDTO fieldUpdateDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            field.Name = fieldUpdateDTO.Name;
            field.FieldType = fieldUpdateDTO.FieldType;
            field.FloorType = fieldUpdateDTO.FloorType;
            field.HourPrice = fieldUpdateDTO.HourPrice;
            field.Illumination = fieldUpdateDTO.Illumination;
            field.Covered = fieldUpdateDTO.Covered;
            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> UpdateTimeSlotsFieldAsync(int fieldId, UpdateTimeSlotFieldRequestDTO updateTimeSlotFieldRequestDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            var slotsComplex = complex.TimeSlots;

            var slots = updateTimeSlotFieldRequestDTO.TimeSlotsField;


            if (slots.Count != 7)
                throw new BadRequestException("Debes enviar exactamente 7 franjas horarias (una por día)");

            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
                throw new BadRequestException("Los días de la semana no pueden repetirse");

            foreach (var fieldSlot in slots)
            {

                var day = fieldSlot.WeekDay;

                var complexSlot = complex.TimeSlots.FirstOrDefault(s => s.WeekDay == day);
                if (complexSlot == null)
                {
                    throw new BadRequestException(
                        $"El complejo no tiene horario configurado para el día {day}"
                    );
                }

                var cInit = complexSlot.StartTime.ToTimeSpan();
                var cEnd = complexSlot.EndTime.ToTimeSpan();
                var fInit = fieldSlot.StartTime.ToTimeSpan();
                var fEnd = fieldSlot.EndTime.ToTimeSpan();

                // cerrado = init == end
                bool complexClosed = cInit == cEnd;
                bool fieldClosed = fInit == fEnd;

                // si el complejo está cerrado, la cancha también debe estarlo
                if (complexClosed)
                {
                    if (!fieldClosed)
                    {
                        throw new BadRequestException(
                            $"La cancha debe estar cerrada el día {day} porque el complejo está cerrado"
                        );
                    }
                    continue;
                }

                // si la cancha está cerrada y el complejo abierto → OK
                if (fieldClosed)
                    continue;

                // cierre post medianoche
                if (cEnd <= cInit)
                    cEnd = cEnd.Add(TimeSpan.FromDays(1));

                if (fEnd <= fInit)
                    fEnd = fEnd.Add(TimeSpan.FromDays(1));

                // validaciones de rango
                if (fInit < cInit)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede abrir antes que el complejo ({fieldSlot.StartTime} < {complexSlot.StartTime})"
                    );
                }

                if (fEnd > cEnd)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede cerrar después que el complejo ({fieldSlot.EndTime} > {complexSlot.EndTime})"
                    );
                }

            }
            foreach (var slotDto in slots)
            {
                var existing = field.TimeSlotsField.First(ts => ts.WeekDay == slotDto.WeekDay);
                existing.StartTime = slotDto.StartTime;
                existing.EndTime = slotDto.EndTime;
            }
            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> UpdateFieldStateAsync(int fieldId, UpdateFieldStateRequestDTO updateFieldStatusRequestDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);
            if(field.FieldState == updateFieldStatusRequestDTO.FieldState)
            {
                throw new BadRequestException("El estado de la cancha es el mismo que se quiere actualizar");
            }
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);
            field.FieldState = updateFieldStatusRequestDTO.FieldState;
            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task DeleteFieldAsync(int fieldId)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            if (await _fieldRepository.HasActiveReservationsInFieldAsync(fieldId))
            {
              throw new BadRequestException("No se puede eliminar la cancha porque tiene reservas activas asociadas");
            }
            field.Active = false;
            await _fieldRepository.SaveAsync();
        }

        public async Task<FieldDetailResponseDTO> AddRecurringFieldBlockAsync(int fieldId, RecurringFieldBlockRequestDTO recurringBlockDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            int newStart = recurringBlockDTO.StartTime.Hour * 60 + recurringBlockDTO.StartTime.Minute;
            int newEnd = recurringBlockDTO.EndTime.Hour * 60 + recurringBlockDTO.EndTime.Minute;

            if (newEnd <= newStart) newEnd += 1440;
            foreach (var rbe in field.RecurringCourtBlocks.Where(b => b.WeekDay == recurringBlockDTO.WeekDay))
            {
                int existStart = rbe.StartTime.Hour * 60 + rbe.StartTime.Minute;
                int existEnd = rbe.EndTime.Hour * 60 + rbe.EndTime.Minute;

                if (existEnd <= existStart) existEnd += 1440;

                bool solapamiento = newStart < existEnd && newEnd > existStart;

                if(solapamiento)
                {
                    throw new BadRequestException("No se puede crear el bloqueo porque se superpone con otro existente");
                }
            }

            var futureReservations = await _reservationRepository.GetActiveReservationsByFieldId(fieldId);

            var conflictingReservations = futureReservations.Where(r => {
                // Validar mismo día de la semana
                if (_complexBusinessLogic.ConvertToWeekDay(r.Date) != recurringBlockDTO.WeekDay)
                    return false;

                int resStart = r.StartTime.Hour * 60 + r.StartTime.Minute;
                int resEnd = resStart + 60; 


                bool conflictToday = resStart < newEnd && resEnd > newStart;

                int resStartNextDay = resStart + 1440;
                int resEndNextDay = resEnd + 1440;
                bool conflictNextDay = resStartNextDay < newEnd && resEndNextDay > newStart;

                return conflictToday || conflictNextDay;
            }).ToList();

            if (conflictingReservations.Any())
            {
                throw new BadRequestException("No se puede crear el bloqueo ya que existen reservas pendientes, aprobadas o bloqueos específicos que se superponen.");
            }
            RecurringFieldBlock recurringFieldBlock = FieldMapper.ToRecurringFieldBlock(recurringBlockDTO);
            field.RecurringCourtBlocks.Add(recurringFieldBlock);
            await _fieldRepository.SaveAsync();

            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> DeleteRecurringFieldBlockAsync(int fieldId, int idrb)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            var recurringBlockExisting = field.RecurringCourtBlocks.FirstOrDefault(f => f.Id == idrb);

            if (recurringBlockExisting == null)
                throw new NotFoundException($"El bloqueo recurrente con el id {idrb} no existe");

            field.RecurringCourtBlocks.Remove(recurringBlockExisting);
            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<Field?> FieldValidityCheck(int fieldId, int complexId)
        {
            var field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no existe");
            }

            if (field.ComplexId != complexId)
            {
                throw new NotFoundException($"La cancha {fieldId} no pertenece al complejo {complexId}");
            }
            return field;
        }

        public async Task<Field?> GetFieldWithRelationsOrThrow(int fieldId)
        {
            var field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no existe");
            }
            return field;
        }
        public async Task<Field?> GetFieldWithRelationsOrThrow2(int fieldId)
        {
            var field = await _fieldRepository.GetFieldByIdWithRelations2Async(fieldId);
            if (field == null)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no existe");
            }
            return field;
        }

        public void ValidateStatusField(Field field)
        {
            if (field.FieldState == FieldState.Deshabilitada)
                throw new BadRequestException($"La cancha con id {field.Id} esta inhabilitada");
        }
    }
}
