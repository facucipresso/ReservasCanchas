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
            if (service == null) return null;

            if (service.Active == false) return null; //ver como devolver eso para diferenciarlo de cuando no existe
            
            var serviceDTO = ServiceMapper.ToServiceResponseDTO(service);
            return serviceDTO;
        }

        public async Task<List<ServiceResponseDTO>> getAll()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            
            var servicesDto = services
                .Where(s => s.Active)
                .Select(ServiceMapper.ToServiceResponseDTO)
                .ToList();

            return servicesDto;
        }

        public async Task<ServiceResponseDTO?> create(ServiceRequestDTO serviceDTO)
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            foreach (var item in services)
            {
                if(item.ServiceDescription == serviceDTO.ServiceDescription)
                {
                    return null; //excepcion de servicio ya existente
                }
            }

            var service = ServiceMapper.ToService(serviceDTO);
            service.Active = true;
            await _serviceRepository.AddServiceAsync(service);
            await _context.SaveChangesAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }

        public async Task<ServiceResponseDTO?> update(int id, ServiceRequestDTO serviceDTO)
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            foreach (var item in services)
            {
                if (item.ServiceDescription == serviceDTO.ServiceDescription)
                {
                    return null; //excepcion de servicio ya existente
                }
            }
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null) return null; // servicio no encontrado

            service.ServiceDescription = serviceDTO.ServiceDescription;
            await _context.SaveChangesAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }

        public async Task<ServiceResponseDTO?> delete(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null) return null; // servicio no encontrado

            service.Active = false;
            await _context.SaveChangesAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }
    }
}
