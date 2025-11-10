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

        public ServiceBusinessLogic(AppDbContext contnext)
        {
            _context = contnext;
        }

        public async Task<List<ServiceDto>> getAll()
        {
            var services = await _context.Service.ToListAsync();
            
            var servicesDto = services
                .Select(ServiceMapper.FromServiceToServiceDto)
                .ToList();

            return servicesDto;
        }

        public async Task<Service?> create(ServiceDto serviceDto)
        {
            var service = ServiceMapper.FromServiceDtoToService(serviceDto);
            var result = await _context.Service.AddAsync(service);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ServiceDto?> GetById(int id)
        {
            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return null;
            }
            var serviceDto = ServiceMapper.FromServiceToServiceDto(service);
            return serviceDto;
        }
    }
}
