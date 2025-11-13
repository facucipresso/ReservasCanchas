using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ComplexBusinessLogic
    {
        private readonly ComplexRepository _complexRepository;

        public ComplexBusinessLogic(ComplexRepository complexRepository)
        {
            _complexRepository = complexRepository;
        }

        public async Task<List<ComplexCardResponseDTO>> GetComplexesForAdminComplexAsync(int adminComplexId)
        {
            List<Complejo> complexes = await _complexRepository.getComplexesByUserIdAsync(adminComplexId);
            var complexescardsdto = complexes.Select(ComplexMapper.toComplexCardResponseDTO).ToList();
            return complexescardsdto;
        }
    }
}
