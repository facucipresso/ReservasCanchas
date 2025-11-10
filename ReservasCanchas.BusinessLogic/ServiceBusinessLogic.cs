using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ServiceBusinessLogic
    {
        private readonly AppDbContext _context;
        //ver como invovo a Domain

        public ServiceBusinessLogic(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceResponseDTO>> getAll()
        {
            var services = await _context.Service.ToListAsync();
            
            var servicesDto = services
                .Select(ServiceMapper.ToServiceResponseDTO)
                .ToList();

            return servicesDto;
        }

        public async Task<ServiceResponseDTO> create(ServiceRequestDTO serviceDTO)
        {
            var service = ServiceMapper.ToService(serviceDTO);
            _context.Service.AddAsync(service);
            await _context.SaveChangesAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }

        public async Task<ServiceResponseDTO?> GetById(int id)
        {
            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return null;
            }
            var serviceDTO = ServiceMapper.ToServiceResponseDTO(service);
            return serviceDTO;
        }
    }
}
