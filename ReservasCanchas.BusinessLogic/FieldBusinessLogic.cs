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
        public FieldBusinessLogic(FieldRepository fieldRepository, ComplexBusinessLogic complexBusinessLogic)
        {
            _fieldRepository = fieldRepository;
            _complexBusinessLogic = complexBusinessLogic;
        }
        public async Task<FieldDetailResponseDTO> GetFieldByIdAsync(int fieldId)
        { //Este metodo es para cuando el admin del complejo quiere ver una cancha en particular.
            var field = await GetFieldWithRelationsOrThrow(fieldId);
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);

            //var userId = _authService.GetUserIdFromToken();
            var userId = 1;
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllFieldsByComplexIdAsync(int complexId)
        {
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            //var userId = _authService.GetUserIdFromToken();
            int userId = 1;
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
                    .Where(f => f.FieldState == FieldState.Habilitado)
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsResponse;
            }
        }

        public async Task<FieldDetailResponseDTO> CreateFieldAsync(CreateFieldRequestDTO createFieldDTO)
        {
            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(createFieldDTO.ComplexId);
            //var userId = _authService.GetUserIdFromToken();
            int userId = 2;
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

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


            foreach (var slot in slots)
            {
                if(slot.EndTime <= slot.InitTime)
                {
                    throw new BadRequestException($"El horario de fin debe ser mayor al horario de inicio para el día {slot.WeekDay}");
                }
            }
            var field = FieldMapper.ToField(createFieldDTO);
            field.Name = $"Cancha {await _fieldRepository.CountFieldsByComplexAsync(complex.Id) + 1} {field.FieldType}";
            field.Active = true;
            field.FieldState = FieldState.Habilitado;
            await _fieldRepository.CreateFieldAsync(field);

            field.RecurringCourtBlocks ??= new List<RecurringFieldBlock>();

            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> UpdateFieldAsync(int fieldId, UpdateFieldRequestDTO fieldUpdateDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            //var userId = _authService.GetUserIdFromToken();
            int userId = 2;
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            if (fieldUpdateDTO.HourPrice.HasValue)
            {
                field.HourPrice = fieldUpdateDTO.HourPrice.Value;
            }
            if (fieldUpdateDTO.Ilumination.HasValue)
            {
                field.Ilumination = fieldUpdateDTO.Ilumination.Value;
            }
            if(fieldUpdateDTO.Covered.HasValue)
            {
                field.Covered = fieldUpdateDTO.Covered.Value;
            }
            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> UpdateTimeSlotsFieldAsync(int fieldId, UpdateTimeSlotFieldRequestDTO updateTimeSlotFieldRequestDTO)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            //var userId = _authService.GetUserIdFromToken();
            int userId = 2;
            _complexBusinessLogic.ValidateOwnerShip(complex, userId);
            _complexBusinessLogic.ValidateFieldOperationsAllowed(complex);

            var slots = updateTimeSlotFieldRequestDTO.TimeSlotsField;
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
                var existing = field.TimeSlotsField.First(ts => ts.WeekDay == slot.WeekDay);
                existing.InitTime = slot.InitTime;
                existing.EndTime = slot.EndTime;
            }

            await _fieldRepository.SaveAsync();
            return FieldMapper.ToFieldDetailResponseDTO(field);

        }

        public async Task DeleteFieldAsync(int fieldId)
        {
            var field = await GetFieldWithRelationsOrThrow(fieldId);

            var complex = await _complexBusinessLogic.GetComplexBasicOrThrow(field.ComplexId);
            //var userId = _authService.GetUserIdFromToken();
            int userId = 1;
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
            //var userId = _authService.GetUserIdFromToken();
            int userId = 2;
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
            //var userId = _authService.GetUserIdFromToken();
            int userId = 2;
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
            if (field.FieldState == FieldState.Deshabilitado)
                throw new BadRequestException($"La cancha con id {field.Id} esta inhabilitada");
        }
    }
}
