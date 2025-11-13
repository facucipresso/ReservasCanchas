using Microsoft.EntityFrameworkCore;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Exceptions;
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

        private readonly ServiceRepository _serviceRepository;

        public ServiceBusinessLogic( ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceResponseDTO?> GetById(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);

            if (service == null)
            {
                throw new NotFoundException("Servicio con id " + id + " no encontrado");
            }

            var serviceDTO = ServiceMapper.ToServiceResponseDTO(service);
            return serviceDTO;
        }

        public async Task<List<ServiceResponseDTO>> GetAll()
        {
            var services = await _serviceRepository.GetAllServicesAsync();
            
            var servicesDto = services
                .Select(ServiceMapper.ToServiceResponseDTO)
                .ToList();

            return servicesDto;
        }

        public async Task<ServiceResponseDTO> Create(ServiceRequestDTO serviceDTO)
        {
            if (await _serviceRepository.ExistsByNameAsync(serviceDTO.ServiceDescription))
            {
                throw new BadRequestException("Ya existe un servicio con la descripcion " + serviceDTO.ServiceDescription);
            }

            var service = ServiceMapper.ToService(serviceDTO);
            service.Active = true;
            await _serviceRepository.CreateServiceAsync(service);
            return ServiceMapper.ToServiceResponseDTO(service);
        }

        public async Task<ServiceResponseDTO> Update(int id, ServiceRequestDTO serviceDTO)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null) 
            { 
                throw new NotFoundException("Servicio con id " + id + " no encontrado");
            }

            if (await _serviceRepository.ExistsByNameAsync(serviceDTO.ServiceDescription)) 
            {
                throw new BadRequestException("Ya existe un servicio con la descripcion " + serviceDTO.ServiceDescription);
            }

            service.ServiceDescription = serviceDTO.ServiceDescription;
            await _serviceRepository.SaveAsync();
            return ServiceMapper.ToServiceResponseDTO(service);
        }

        public async Task Delete(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null)
            {
                throw new NotFoundException("Servicio con id " + id + " no encontrado");
            }

            service.Active = false;
            await _serviceRepository.SaveAsync();
        }
    }
}
