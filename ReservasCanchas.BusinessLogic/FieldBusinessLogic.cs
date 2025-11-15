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
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class FieldBusinessLogic
    {
        private readonly FieldRepository _fieldRepository;
        private readonly ComplexRepository _complexRepository;
        public FieldBusinessLogic(FieldRepository fieldRepository, ComplexRepository complexRepository)
        {
            _fieldRepository = fieldRepository;
            _complexRepository = complexRepository;
        }
        public async Task<FieldResponseDTO> GetById(int id)
        {
            var field = await _fieldRepository.GetFieldByIdAsync(id);
            if (field == null)
            {
                throw new Exception("Cancha con id " + id + " no encontrada");
            }
            var fieldDTO = FieldMapper.ToFieldResponseDTO(field);
            return fieldDTO;
        }

        public async Task<List<FieldResponseDTO>> GetAllByComplexId(int complexId)
        {
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);
            if (complex == null)
            {
                throw new NotFoundException("Complejo con id " + complexId + " no encontrado");
            }
            var fields = await _fieldRepository.GetFieldsByComplexIdAsync(complexId);
            var fieldsDTO = fields
                .Select(FieldMapper.ToFieldResponseDTO)
                .ToList();
            return fieldsDTO;
        }

        public async Task<FieldResponseDTO> Create(int complexId, FieldRequestDTO fieldDTO)
        {
            var complex = await _complexRepository.GetComplexByIdAsync(complexId);
            if (complex == null)
            {
                throw new NotFoundException("Complejo con id " + complexId + " no encontrado");
            }
            var field = FieldMapper.ToField(fieldDTO);
            field.Name = $"Cancha {await _fieldRepository.CountFieldsByComplexAsync(complexId) + 1} {field.FieldType}";
            field.Active = true;
            await _fieldRepository.CreateFieldAsync(field);
            return FieldMapper.ToFieldResponseDTO(field);
        }

        public async Task<FieldResponseDTO> Update(int fieldId, FieldUpdateRequestDTO fieldUpdateDTO)
        {
            var field = await _fieldRepository.GetFieldByIdAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException("Cancha con id " + fieldId + " no encontrada");
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
            return FieldMapper.ToFieldResponseDTO(field);
        }

        public async Task Delete (int fieldId)
        {
            var field = await _fieldRepository.GetFieldByIdAsync(fieldId);
            if (field == null)
            {
                throw new NotFoundException("Cancha con id " + fieldId + " no encontrada");
            }
            field.Active = false;
            await _fieldRepository.SaveAsync();
        }

        public async Task<FieldResponseDTO> AddRecurridFieldBlockAsinc(int adminComplexId, int complexId, int id, RecurringFieldBlockRequestDTO recurridBlockDTO)
        {
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
    }
}
