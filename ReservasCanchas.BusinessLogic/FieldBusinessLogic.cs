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
        public async Task<FieldDetailResponseDTO> GetById(int complexId, int fieldId)
        { //Este metodo seria para cuando el admin del complejo quiere ver una cancha en particular y editarla o hacer algo.
            var field = await FieldValidityCheck(fieldId, complexId);
            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);
            var fieldDTO = FieldMapper.ToFieldDetailResponseDTO(field);
            return fieldDTO;
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllByComplexId(int complexId)
        {
            var complex = await _complexBusinessLogic.ComplexValidityExistenceCheck(complexId);
            //obtener complexAdminId del token
            int complexAdminId = 1;
            if (complex.UserId == complexAdminId)
            {
                var fields = await _fieldRepository.GetAllFieldsByComplexIdWithRelationsAsync(complexId);
                var fieldsDTO = fields
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsDTO;
            }
            else
            {
                var fields = await _fieldRepository.GetAllFieldsByComplexIdWithRelationsAsync(complexId);
                var fieldsDTO = fields
                    .Where(f => f.FieldState == FieldState.Habilitado)
                    .Select(FieldMapper.ToFieldDetailResponseDTO)
                    .ToList();
                return fieldsDTO;
            }
        }

        public async Task<FieldDetailResponseDTO> Create(int complexId, FieldRequestDTO fieldDTO)
        {
            var complex = await _complexBusinessLogic.ComplexValidityExistenceCheck(complexId);
            _complexBusinessLogic.ComplexValidityStateCheck(complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(complex, adminComplexId);
            var slots = fieldDTO.TimeSlotsField;
            if (slots.Select(s => s.WeekDay).Distinct().Count() != 7)
            {
                throw new BadRequestException($"No se pueden repetir dias de la semana en los horarios del complejo");
            }
            foreach (var slot in slots)
            {
                if(slot.EndTime <= slot.InitTime)
                {
                    throw new BadRequestException($"El horario de fin debe ser mayor al horario de inicio para el día {slot.WeekDay}");
                }
            }
            var field = FieldMapper.ToField(fieldDTO);
            field.Name = $"Cancha {await _fieldRepository.CountFieldsByComplexAsync(complexId) + 1} {field.FieldType}";
            field.Active = true;
            field.FieldState = FieldState.Habilitado;
            await _fieldRepository.CreateFieldAsync(field);
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> Update(int complexId, int fieldId, FieldUpdateRequestDTO fieldUpdateDTO)
        {
            var field = await FieldValidityCheck(fieldId, complexId);

            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);

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

        public async Task<FieldDetailResponseDTO> UpdateTimeSlots(int complexId, int fieldId, UpdateTimeSlotFieldRequestDTO updateTimeSlotFieldRequestDTO)
        {
            var field = await FieldValidityCheck(fieldId, complexId);
            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);

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

        public async Task Delete (int complexId, int fieldId)
        {
            //QUE HACER SI LA CANCHA TIENE RESERVAS SIN COMPLETAR?
            var field = await FieldValidityCheck(fieldId, complexId);


            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);

            if (await _fieldRepository.HasActiveReservationsInFieldAsync(fieldId))
            {
              throw new BadRequestException("No se puede eliminar la cancha porque tiene reservas activas asociadas");
            }
            field.Active = false;
            await _fieldRepository.SaveAsync();
        }

        public async Task<FieldDetailResponseDTO> AddRecurringFieldBlockAsync(int complexId, int fieldId, RecurringFieldBlockRequestDTO recurringBlockDTO)
        {
            //QUE HACER SI SE CREA UN BLOQUEO RECURRENTE Y LA CANCHA TIENE RESERVAS SIN COMPLETAR EN ESE HORARIO?
            var field = await FieldValidityCheck(fieldId, complexId);
            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);

            if (recurringBlockDTO.InitHour >= recurringBlockDTO.EndHour) throw new BadRequestException("La hora inicial debe ser menor que la hora final");

            var recurringBlockExisting = field.RecurringCourtBlocks;
            foreach (var rbe in recurringBlockExisting)
            {
                bool solapamiento = rbe.WeekDay == recurringBlockDTO.WeekDay && rbe.InitHour < recurringBlockDTO.EndHour && rbe.EndHour > recurringBlockDTO.InitHour;
                if(solapamiento)
                {
                    throw new BadRequestException("No se puede crear el bloqueo porque se superpone con otro existente");
                }
            }
            field.RecurringCourtBlocks.Add(new RecurringFieldBlock
            {
                FieldId = fieldId,
                WeekDay = recurringBlockDTO.WeekDay,
                InitHour = recurringBlockDTO.InitHour,
                EndHour = recurringBlockDTO.EndHour,
                Reason = recurringBlockDTO.Reason
             });
            await _fieldRepository.SaveAsync();

            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> DeleteRecurringFieldBlockAsync(int complexId, int fieldId, int idrb)
        {
            var field = await FieldValidityCheck(fieldId, complexId);
            _complexBusinessLogic.ComplexValidityStateCheck(field.Complex);
            //Obtenemos el adminId del token
            //var adminComplexId = _authService.GetUserIdFromToken();
            var adminComplexId = 1;
            _complexBusinessLogic.ComplexValidityAdmin(field.Complex, adminComplexId);
            var recurringBlockExisting = field.RecurringCourtBlocks.FirstOrDefault(f => f.Id == idrb);
            if (recurringBlockExisting == null) throw new NotFoundException($"El bloqueo recurrente con el id {idrb} no existe");
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
    }
}
