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
                Covered = field.Covered,
                FieldState = field.FieldState
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
                TimeSlotsField = fieldRequestDTO.TimeSlotsField.Select(toTimeSlotField).ToList()
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
                FieldState = field.FieldState,
                TimeSlotsField = field.TimeSlotsField.Select(toTimeSlotFieldResponseDTO).ToList(),
                RecurringCourtBlocks = field.RecurringCourtBlocks.Select(toRecurringFieldBlockResponseDTO).ToList()
            };
        }

        public static TimeSlotFieldResponseDTO toTimeSlotFieldResponseDTO(TimeSlotField timeSlotField)
        {
            return new TimeSlotFieldResponseDTO
            {
                Id = timeSlotField.Id,
                FieldId = timeSlotField.FieldId,
                WeekDay = timeSlotField.WeekDay,
                InitTime = timeSlotField.InitTime,
                EndTime = timeSlotField.EndTime
            };
        }

        public static TimeSlotField toTimeSlotField(TimeSlotFieldRequestDTO timeSlotFieldRequestDTO)
        {
            return new TimeSlotField
            {
                WeekDay = timeSlotFieldRequestDTO.WeekDay,
                InitTime = timeSlotFieldRequestDTO.InitTime,
                EndTime = timeSlotFieldRequestDTO.EndTime
            };
        }

        public static RecurringFieldBlockResponseDTO toRecurringFieldBlockResponseDTO(RecurringFieldBlock recurringFieldBlock)
        {
            return new RecurringFieldBlockResponseDTO
            {
                Id = recurringFieldBlock.Id,
                FieldId = recurringFieldBlock.FieldId,
                WeekDay = recurringFieldBlock.WeekDay,
                InitHour = recurringFieldBlock.InitHour,
                EndHour = recurringFieldBlock.EndHour,
                Reason = recurringFieldBlock.Reason
            };
        }

        public static RecurringFieldBlock toRecurringFieldBlock(RecurringFieldBlockRequestDTO recurringFieldBlockRequestDTO)
        {
            return new RecurringFieldBlock
            {
                WeekDay = recurringFieldBlockRequestDTO.WeekDay,
                InitHour = recurringFieldBlockRequestDTO.InitHour,
                EndHour = recurringFieldBlockRequestDTO.EndHour,
                Reason = recurringFieldBlockRequestDTO.Reason
            };
        }
    }
}
