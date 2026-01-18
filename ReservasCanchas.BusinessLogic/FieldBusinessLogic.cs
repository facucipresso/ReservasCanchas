using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class FieldBusinessLogic
    {
        private readonly FieldRepository _fieldRepository;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly AuthService _authService;
        public FieldBusinessLogic(FieldRepository fieldRepository, ComplexBusinessLogic complexBusinessLogic, AuthService authService)
        {
            _fieldRepository = fieldRepository;
            _complexBusinessLogic = complexBusinessLogic;
            _authService = authService;
        }
        public async Task<FieldDetailResponseDTO> GetFieldByIdAsync(int fieldId)
        { //Este metodo es para cuando el admin del complejo quiere ver una cancha en particular.
            var field = await GetFieldWithRelationsOrThrow(fieldId);
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);

            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllFieldsByComplexIdAsync(int complexId)
        {
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var userId = _authService.GetUserId();
            var fields = await _fieldRepository.GetAllFieldsByComplexIdWithRelationsAsync(complexId);
            if (complex.UserId == userId) //si es admin devolvemos todas las canchas en todos los estados y sin importar el estado del complejo
            {
                var fieldsResponse = fields
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsResponse;
            }
            else
            { //si no es admin devolvemos solo las canchas habilitadas y si el complejo esta habilitado
                _complexBusinessLogic.ValidateAccessForBasicUser(complex);
                var fieldsResponse = fields
                    .Where(f => f.FieldState == FieldState.Habilitada)
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsResponse;
            }
        }

        public async Task<FieldDetailResponseDTO> CreateFieldAsync(CreateFieldRequestDTO createFieldDTO)
        {
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(createFieldDTO.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            var slotsComplex = complex.TimeSlots;

            var slots = createFieldDTO.TimeSlotsField;
            /*
            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
            {
                throw new BadRequestException($"No se pueden repetir dias de la semana en los horarios del complejo");
            }
            */

            if (slots.Count != 7)
                throw new BadRequestException("Debes enviar exactamente 7 franjas horarias (una por día)");

            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
                throw new BadRequestException("Los días de la semana no pueden repetirse");

            //aca hay que cheachear que el time slot sea dentro del horario de apertura y cierre del complejo
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

                var cInit = complexSlot.InitTime.ToTimeSpan();
                var cEnd = complexSlot.EndTime.ToTimeSpan();
                var fInit = fieldSlot.InitTime.ToTimeSpan();
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

                // normalización de cierre post medianoche
                if (cEnd <= cInit)
                    cEnd = cEnd.Add(TimeSpan.FromDays(1));

                if (fEnd <= fInit)
                    fEnd = fEnd.Add(TimeSpan.FromDays(1));

                // validaciones de rango
                if (fInit < cInit)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede abrir antes que el complejo ({fieldSlot.InitTime} < {complexSlot.InitTime})"
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
            field.Ilumination = fieldUpdateDTO.Ilumination;
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
            /*
            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
            {
                throw new BadRequestException($"No se pueden repetir dias de la semana en los horarios del complejo");
            }
            */

            if (slots.Count != 7)
                throw new BadRequestException("Debes enviar exactamente 7 franjas horarias (una por día)");

            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
                throw new BadRequestException("Los días de la semana no pueden repetirse");

            //aca hay que cheachear que el time slot sea dentro del horario de apertura y cierre del complejo
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

                var cInit = complexSlot.InitTime.ToTimeSpan();
                var cEnd = complexSlot.EndTime.ToTimeSpan();
                var fInit = fieldSlot.InitTime.ToTimeSpan();
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

                // normalización de cierre post medianoche
                if (cEnd <= cInit)
                    cEnd = cEnd.Add(TimeSpan.FromDays(1));

                if (fEnd <= fInit)
                    fEnd = fEnd.Add(TimeSpan.FromDays(1));

                // validaciones de rango
                if (fInit < cInit)
                {
                    throw new BadRequestException(
                        $"La cancha el día {day} no puede abrir antes que el complejo ({fieldSlot.InitTime} < {complexSlot.InitTime})"
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
                existing.InitTime = slotDto.InitTime;
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
            //QUE HACER SI SE CREA UN BLOQUEO RECURRENTE Y LA CANCHA TIENE RESERVAS SIN COMPLETAR EN ESE HORARIO?
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            var userId = _authService.GetUserId();
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            if (recurringBlockDTO.InitHour >= recurringBlockDTO.EndHour) 
                throw new BadRequestException("La hora inicial debe ser menor que la hora final");

            var recurringBlockExisting = field.RecurringCourtBlocks;
            foreach (var rbe in recurringBlockExisting)
            {
                bool solapamiento = rbe.WeekDay == recurringBlockDTO.WeekDay && rbe.InitHour < recurringBlockDTO.EndHour && rbe.EndHour > recurringBlockDTO.InitHour;
                if(solapamiento)
                {
                    throw new BadRequestException("No se puede crear el bloqueo porque se superpone con otro existente");
                }
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

        public void ValidateStatusField(Field field)
        {
            if (field.FieldState == FieldState.Deshabilitada)
                throw new BadRequestException($"La cancha con id {field.Id} esta inhabilitada");
        }
    }
}
