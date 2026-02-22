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
        public static ComplexCardResponseDTO toComplexCardResponseDTO(Complex complex, decimal lowestPrice)
        {
            return new ComplexCardResponseDTO
            {
                Id = complex.Id,
                UserId = complex.UserId,
                Name = complex.Name,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                ComplexState = complex.ComplexState,
                ImagePath = complex.ImagePath,
                LowestPricePerField = lowestPrice
            };
        }

        public static ComplexDetailResponseDTO toComplexDetailResponseDTO(Complex complex)
        {
            return new ComplexDetailResponseDTO
            {
                Id = complex.Id,
                Name = complex.Name,
                Description = complex.Description,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                Phone = complex.Phone,
                ImagePath = complex.ImagePath,
                PercentageSign = complex.PercentageSign,
                StartIllumination = complex.StartIllumination,
                AditionalIllumination = complex.AditionalIllumination,
                CancelationReason = complex.CancelationReason,
                CBU = complex.CBU,
                ComplexState = complex.ComplexState,
                Services = complex.Services.Select(ServiceMapper.ToServiceResponseDTO).ToList(),
                TimeSlots = complex.TimeSlots.Select(ToTimeSlotComplexResponseDTO).ToList()
            };
        }
        public static ComplexSuperAdminResponseDTO toComplexSuperAdminResponseDTO(Complex complex, string name, string lastname)
        {
            return new ComplexSuperAdminResponseDTO
            {
                Id = complex.Id,
                UserId = complex.UserId,
                NameUser = name,
                LastNameUser = lastname,
                Name = complex.Name,
                Province = complex.Province,
                Locality = complex.Locality,
                Street = complex.Street,
                Number = complex.Number,
                Phone = complex.Phone,
                ComplexState = complex.ComplexState,
            };
        }

        public static Complex toComplex(CreateComplexRequestDTO complexDTO, int userId)
        {
            return new Complex
            {
                UserId = userId,
                Name = complexDTO.Name,
                Description = complexDTO.Description,
                Province = complexDTO.Province,
                Locality = complexDTO.Locality,
                Street = complexDTO.Street,
                Number = complexDTO.Number,
                Phone = complexDTO.Phone,
                PercentageSign = complexDTO.PercentageSign,
                StartIllumination = complexDTO.StartIllumination,
                AditionalIllumination = complexDTO.AditionalIllumination,
                CBU = complexDTO.CBU,
                TimeSlots = complexDTO.TimeSlots.Select(ToTimeSlotComplex).ToList()
                /*TimeSlots = complexDTO.TimeSlotsList.Select(ts => new TimeSlotComplex
                {
                    WeekDay = ts.WeekDay,
                    StartTime = ts.StartTime,
                    EndTime = ts.EndTime
                }).ToList()
                */
            };
        }

        public static TimeSlotComplex ToTimeSlotComplex(TimeSlotComplexRequestDTO timeSlotDTO)
        {
            return new TimeSlotComplex
            {
                WeekDay = timeSlotDTO.WeekDay,
                StartTime = timeSlotDTO.StartTime,
                EndTime = timeSlotDTO.EndTime
            };
        }

        public static TimeSlotComplexResponseDTO ToTimeSlotComplexResponseDTO(TimeSlotComplex timeSlot)
        {
            return new TimeSlotComplexResponseDTO
            {
                Id = timeSlot.Id,
                WeekDay = timeSlot.WeekDay,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime
            };
        }
    }
}
