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

        public static FieldDetailResponseDTO ToFieldDetailResponseDTO(Field field)
        {
            return new FieldDetailResponseDTO
            {
                Id = field.Id,
                Name = field.Name,
                ComplexId = field.ComplexId,
                FieldType = field.FieldType,
                FloorType = field.FloorType,
                HourPrice = field.HourPrice,
                Ilumination = field.Ilumination,
                Covered = field.Covered,
                Active = field.Active,
                TimeSlotsField = field.TimeSlotsField.Select(toTimeSlotFieldResponseDTO).ToList(),
                RecurringCourtBlocks = field.RecurringCourtBlocks.Select(toRecurringFieldBlockResponseDTO).ToList()
            };
        }

        public static TimeSlotFieldResponseDTO toTimeSlotFieldResponseDTO(TimeSlotField timeSlotField)
        {
            return null;
        }

        public static TimeSlotField toTimeSlotField(TimeSlotFieldRequestDTO timeSlotFieldRequestDTO)
        {
            return null;
        }

        public static RecurringFieldBlockResponseDTO toRecurringFieldBlockResponseDTO(RecurringFieldBlock recurringFieldBlock)
        {
            return null;
        }

        public static RecurringFieldBlock toRecurringFieldBlock(RecurringFieldBlockRequestDTO recurringFieldBlockRequestDTO)
        {
            return null;
        }
    }
}
