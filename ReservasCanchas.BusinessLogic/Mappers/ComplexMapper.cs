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
        public static ComplexCardResponseDTO toComplexCardResponseDTO(Complejo complex)
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
        public static ComplexSuperAdminResponseDTO toComplexSuperAdminResponseDTO(Complejo complex)
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
    }
}
