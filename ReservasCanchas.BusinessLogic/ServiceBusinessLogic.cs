using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.DataAccess.Repositories;
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
        private readonly ServiceRepository _serviceRepository;

        public ServiceBusinessLogic(AppDbContext context, ServiceRepository serviceRepository)
        {
            _context = context;
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceResponseDTO?> GetById(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null)
            {
                return null;
            }
            var serviceDTO = ServiceMapper.ToServiceResponseDTO(service);
            return serviceDTO;
        }

        public async Task<List<ServiceResponseDTO>> getAll()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            
            var servicesDto = services
                .Select(ServiceMapper.ToServiceResponseDTO)
                .ToList();

            return servicesDto;
        }

        public async Task<ServiceResponseDTO> create(ServiceRequestDTO serviceDTO)
        {
            var service = ServiceMapper.ToService(serviceDTO);
            _serviceRepository.AddServiceAsync(service);
            await _context.SaveChangesAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }
    }
}
