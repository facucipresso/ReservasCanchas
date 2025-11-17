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
    { //PARA MI ESTA TODO MAL, TENER LOGICA DE COMPLEJO EN LAS CANCHAS??, HAY QUE REVISAR BIEN ESTO
        private readonly FieldRepository _fieldRepository;
        private readonly ComplexRepository _complexRepository;
        public FieldBusinessLogic(FieldRepository fieldRepository, ComplexRepository complexRepository)
        {
            _fieldRepository = fieldRepository;
            _complexRepository = complexRepository;
        }
        public async Task<FieldDetailResponseDTO?> GetById(int complexId, int fieldId)
        {
                
            var field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);

            if (field == null)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no existe en el complejo con id {complexId}");
            }

            if (field.ComplexId != complexId)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no pertenece al complejo con id {complexId}");
            }
            var fieldDTO = FieldMapper.ToFieldDetailResponseDTO(field);
            return fieldDTO;
        }

        public async Task<List<FieldDetailResponseDTO>> GetAllByComplexId(int complexId)
        {
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);
            if (complex == null)
            {
                throw new NotFoundException("Complejo con id " + complexId + " no encontrado");
            }
            if (!complex.Active)
            {
                throw new BadRequestException("El complejo no está activo.");
            }
            if(complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no está habilitado.");
            }
            var fields = await _fieldRepository.GetAllFieldsByComplexIdWithRelationsAsync(complexId);
            var fieldsDTO = fields
                .Select(FieldMapper.ToFieldDetailResponseDTO)
                .ToList();
            return fieldsDTO;
        }

        public async Task<FieldDetailResponseDTO> Create(int complexId, FieldRequestDTO fieldDTO)
        {
            //CHEQUEAR SI EL COMPLEJO PERTENECE AL ADMIN QUE CREA LA CANCHA
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);
            if (complex == null)
            {
                throw new NotFoundException("Complejo con id " + complexId + " no encontrado");
            }
            if (!complex.Active)
            {
                throw new BadRequestException("El complejo no está activo.");
            }
            if (complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no está habilitado.");
            }
            var field = FieldMapper.ToField(fieldDTO);
            field.Name = $"Cancha {await _fieldRepository.CountFieldsByComplexAsync(complexId) + 1} {field.FieldType}";
            field.Active = true;
            await _fieldRepository.CreateFieldAsync(field);
            return FieldMapper.ToFieldDetailResponseDTO(field);
        }

        public async Task<FieldDetailResponseDTO> Update(int complexId, int fieldId, FieldUpdateRequestDTO fieldUpdateDTO)
        {
            //CHEQUEAR SI EL COMPLEJO PERTENECE AL ADMIN QUE MODIFICA LA CANCHA
            var field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException("La cancha con id " + fieldId + " no existe");
            }

            if (field.ComplexId != complexId)
            {
                throw new NotFoundException($"La cancha {fieldId} no pertenece al complejo {complexId}");
            }

            if (!field.Complex.Active)
            {
                throw new BadRequestException("El complejo no está activo.");
            }
            if (field.Complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no está habilitado.");
            }

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

        public async Task<FieldDetailResponseDTO> UpdateTimeSlots(int complexId, int fieldId, int complexAdminId, UpdateTimeSlotFieldRequestDTO updateTimeSlotFieldRequestDTO)
        {
            Field field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);
            if(field == null)
            {
                throw new NotFoundException($"La cancha con id {fieldId} no existe");
            }
            Complejo complex = await _complexRepository.GetComplexByIdAsync(field.ComplexId);
            if(complexAdminId != complex.UserId)
            {
                throw new BadRequestException($"No se pueden actualizar complejos ajenos");
            }

            if (field.ComplexId != complexId)
            {
                throw new NotFoundException($"La cancha {fieldId} no pertenece al complejo {complexId}");
            }

            if (!field.Complex.Active)
            {
                throw new BadRequestException("El complejo no está activo.");
            }
            if (field.Complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no está habilitado.");
            }

            var slots = updateTimeSlotFieldRequestDTO.TimeSlotsField;
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
            //CHEQUEAR SI EL COMPLEJO PERTENECE AL ADMIN QUE ELIMINA LA CANCHA
            var field = await _fieldRepository.GetFieldByIdWithRelationsAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException("La cancha con id " + fieldId + " no existe");
            }

            if (field.ComplexId != complexId)
            {
                throw new NotFoundException($"La cancha {fieldId} no pertenece al complejo {complexId}");
            }

            if (!field.Complex.Active)
            {
                throw new BadRequestException("El complejo no está activo.");
            }
            if (field.Complex.State != ComplexState.Habilitado)
            {
                throw new BadRequestException("El complejo no está habilitado.");
            }
            field.Active = false;
            await _fieldRepository.SaveAsync();
        }

        public async Task<FieldResponseDTO> AddRecurridFieldBlockAsinc(int adminComplexId, int complexId, int id, RecurringFieldBlockRequestDTO recurridBlockDTO)
        {
            //QUE HACER SI SE CREA UN BLOQUEO RECURRENTE Y LA CANCHA TIENE RESERVAS SIN COMPLETAR EN ESE HORARIO?
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);
            if(complex == null) throw new NotFoundException("El complejo con el id " + id + " no existe");

            if (complex.UserId != adminComplexId)
            {
                throw new NotFoundException("No tiene permisos para realizar esta operacion");
            }
            var field = await _fieldRepository.GetFieldByIdAsync(id);
            if(field == null)
            {
                throw new NotFoundException("El cancha con el id " + id + " no existe");
            }
            if(field.ComplexId != complexId)
            {
                throw new NotFoundException("La cancha no pertenece al complejo especificado");
            }

            if(recurridBlockDTO.InitHour >= recurridBlockDTO.EndHour) throw new BadRequestException("La hora inicial debe ser menor que la hora final");

            var recurridBlockExisting = field.RecurringCourtBlocks;
            foreach (var rbe in recurridBlockExisting)
            {
                bool solapamiento = rbe.WeekDay == recurridBlockDTO.WeekDay && rbe.InitHour < recurridBlockDTO.EndHour && rbe.EndHour > recurridBlockDTO.InitHour;
                if(solapamiento)
                {
                    throw new BadRequestException("No se puede crear el bloqueo porque se superpone con otro existente");
                }
            }
            field.RecurringCourtBlocks.Add(new RecurringFieldBlock
            {
                FieldId = id,
                WeekDay = recurridBlockDTO.WeekDay,
                InitHour = recurridBlockDTO.InitHour,
                EndHour = recurridBlockDTO.EndHour,
                Reason = recurridBlockDTO.Reason
             });
            await _fieldRepository.SaveAsync();

            return FieldMapper.ToFieldResponseDTO(field);
        }

        public async Task DeleteRecurridFieldBlockAsinc(int adminComplexId, int complexId, int id, int idrb)
        {
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);

            if (complex == null) throw new NotFoundException($"El complejo con id {complexId} no existe"); 

            if (complex.UserId != adminComplexId)
            {
                throw new NotFoundException("No tiene permisos para realizar esta operacion");
            }
            var field = await _fieldRepository.GetFieldByIdWithBlocksAsync(id);
            if (field == null)
            {
                throw new NotFoundException("La cancha con el id " + id + " no existe");
            }
            if (field.ComplexId != complexId)
            {
                throw new NotFoundException("La cancha no pertenece al complejo especificado");
            }
            var recurridBlockExisting = field.RecurringCourtBlocks.FirstOrDefault(f => f.Id == idrb);
            if (recurridBlockExisting == null) throw new NotFoundException("El bloqueo recurrente con el id " + id + " no existe");

            field.RecurringCourtBlocks.Remove(recurridBlockExisting);
            await _fieldRepository.SaveAsync();
        }

        private void ValidateFieldWithComplex(Field field, int complexId, int adminComplexId)
        {
            if (field == null)
                throw new NotFoundException($"La cancha no existe.");

            var complex = field.Complex;

            if (complex.Id != complexId)
                throw new NotFoundException($"La cancha con id {field.Id} no existe en el complejo con id {complexId}");

            if (!complex.Active)
                throw new BadRequestException("El complejo no está activo.");

            if (complex.State != ComplexState.Habilitado)
                throw new BadRequestException("El complejo no está habilitado.");

            if (complex.UserId != adminComplexId)
                throw new BadRequestException("No tenés permiso para modificar esta cancha.");
        }
    }
}
