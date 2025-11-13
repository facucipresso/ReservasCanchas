using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class ServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            return await _context.Service.FirstOrDefaultAsync(s => s.Id == id && s.Active == true);
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Service
                .Where(s => s.Active)
                .ToListAsync();
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            _context.Service.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task SaveAsync()
        {
            await  _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string serviceDescription)
        {
            return await _context.Service.AnyAsync(s => s.ServiceDescription.ToLower() == serviceDescription.ToLower() && s.Active);
        }

    }
}
