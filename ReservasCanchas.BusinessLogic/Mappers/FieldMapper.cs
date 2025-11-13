using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class FieldMapper
    {
        public static FieldResponseDTO ToFieldResponseDTO(Field field)
        {
            return new FieldResponseDTO
            {
                Id = field.Id,
                Name = field.Name,
                ComplexId = field.ComplexId,
                FieldType = field.FieldType,
                FloorType = field.FloorType,
                HourPrice = field.HourPrice,
                Ilumination = field.Ilumination,
                Covered = field.Covered
            };
        }
        public static Field ToField(FieldRequestDTO fieldRequestDTO)
        {
            return new Field
            {
                ComplexId = fieldRequestDTO.ComplexId,
                FieldType = fieldRequestDTO.FieldType,
                FloorType = fieldRequestDTO.FloorType,
                HourPrice = fieldRequestDTO.HourPrice,
                Ilumination = fieldRequestDTO.Ilumination,
                Covered = fieldRequestDTO.Covered,
            };
        }
    }
}
