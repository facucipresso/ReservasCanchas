using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class ComplexMapper
    {
        public static ComplexCardResponseDTO toComplexCardResponseDTO(Complex complex)
        {
            return new ComplexCardResponseDTO
            {
                Id = complex.Id,
                Name = complex.Name,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                ImagePath = complex.ImagePath
            };
        }

        public static ComplexDetailResponseDTO toComplexDetailResponseDTO(Complex complex)
        {
            return new ComplexDetailResponseDTO
            {
                Id = complex.Id,
                Name = complex.Name,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                Phone = complex.Phone,
                ImagePath = complex.ImagePath,
                PercentageSign = complex.PercentageSign,
                StartIlumination = complex.StartIlumination,
                AditionalIlumination = complex.AditionalIlumination,
                State = complex.State,
                Services = complex.Services.Select(ServiceMapper.ToServiceResponseDTO).ToList(),
                TimeSlots = complex.TimeSlots.Select(ToTimeSlotComplexResponseDTO).ToList()
            };
        }
        public static ComplexSuperAdminResponseDTO toComplexSuperAdminResponseDTO(Complex complex)
        {
            return new ComplexSuperAdminResponseDTO
            {
                Id = complex.Id,
                UserId = complex.UserId,
                Name = complex.Name,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                Phone = complex.Phone,
                State = complex.State,
                Active = complex.Active
            };
        }

        public static Complex toComplex(CreateComplexRequestDTO complexDTO)
        {
            return new Complex
            {
                UserId = complexDTO.UserId,
                Name = complexDTO.Name,
                Province = complexDTO.Province,
                Locality = complexDTO.Locality,
                Street = complexDTO.Street,
                Number = complexDTO.Number,
                Phone = complexDTO.Phone,
                ImagePath = complexDTO.ImagePath,
                PercentageSign = complexDTO.PercentageSign,
                StartIlumination = complexDTO.StartIlumination,
                AditionalIlumination = complexDTO.AditionalIlumination,
                TimeSlots = complexDTO.TimeSlots.Select(ToTimeSlotComplex).ToList()
            };
        }

        public static TimeSlotComplex ToTimeSlotComplex(TimeSlotComplexRequestDTO timeSlotDTO)
        {
            return new TimeSlotComplex
            {
                WeekDay = timeSlotDTO.WeekDay,
                InitTime = timeSlotDTO.InitTime,
                EndTime = timeSlotDTO.EndTime
            };
        }

        public static TimeSlotComplexResponseDTO ToTimeSlotComplexResponseDTO(TimeSlotComplex timeSlot)
        {
            return new TimeSlotComplexResponseDTO
            {
                Id = timeSlot.Id,
                WeekDay = timeSlot.WeekDay,
                InitTime = timeSlot.InitTime,
                EndTime = timeSlot.EndTime
            };
        }
    }
}
