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
        public static Field ToField(CreateFieldRequestDTO fieldRequestDTO)
        {
            return new Field
            {
                ComplexId = fieldRequestDTO.ComplexId,
                Name = fieldRequestDTO.Name,
                FieldType = fieldRequestDTO.FieldType,
                FloorType = fieldRequestDTO.FloorType,
                HourPrice = fieldRequestDTO.HourPrice,
                Illumination = fieldRequestDTO.Illumination,
                Covered = fieldRequestDTO.Covered,
                TimeSlotsField = fieldRequestDTO.TimeSlotsField.Select(ToTimeSlotField).ToList(),
                RecurringCourtBlocks = new List<RecurringFieldBlock>()
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
                Illumination = field.Illumination,
                Covered = field.Covered,
                Active = field.Active,
                FieldState = field.FieldState,
                TimeSlotsField = field.TimeSlotsField.Select(ToTimeSlotFieldResponseDTO).ToList(),
                RecurringCourtBlocks = field.RecurringCourtBlocks.Select(ToRecurringFieldBlockResponseDTO).ToList()
            };
        }

        public static TimeSlotFieldResponseDTO ToTimeSlotFieldResponseDTO(TimeSlotField timeSlotField)
        {
            return new TimeSlotFieldResponseDTO
            {
                Id = timeSlotField.Id,
                FieldId = timeSlotField.FieldId,
                WeekDay = timeSlotField.WeekDay,
                StartTime = timeSlotField.StartTime,
                EndTime = timeSlotField.EndTime
            };
        }

        public static TimeSlotField ToTimeSlotField(TimeSlotFieldRequestDTO timeSlotFieldRequestDTO)
        {
            return new TimeSlotField
            {
                WeekDay = timeSlotFieldRequestDTO.WeekDay,
                StartTime = timeSlotFieldRequestDTO.StartTime,
                EndTime = timeSlotFieldRequestDTO.EndTime
            };
        }

        public static RecurringFieldBlockResponseDTO ToRecurringFieldBlockResponseDTO(RecurringFieldBlock recurringFieldBlock)
        {
            return new RecurringFieldBlockResponseDTO
            {
                Id = recurringFieldBlock.Id,
                FieldId = recurringFieldBlock.FieldId,
                WeekDay = recurringFieldBlock.WeekDay,
                StartTime = recurringFieldBlock.StartTime,
                EndTime = recurringFieldBlock.EndTime,
                Reason = recurringFieldBlock.Reason
            };
        }

        public static RecurringFieldBlock ToRecurringFieldBlock(RecurringFieldBlockRequestDTO recurringFieldBlockRequestDTO)
        {
            return new RecurringFieldBlock
            {
                WeekDay = recurringFieldBlockRequestDTO.WeekDay,
                StartTime = recurringFieldBlockRequestDTO.StartTime,
                EndTime = recurringFieldBlockRequestDTO.EndTime,
                Reason = recurringFieldBlockRequestDTO.Reason
            };
        }
    }
}
