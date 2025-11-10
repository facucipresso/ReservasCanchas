using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
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
            return await _context.Service.FindAsync(id);
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Service.ToListAsync();
        }

        public async Task<EntityEntry<Service>> AddServiceAsync(Service service)
        {
            return await _context.Service.AddAsync(service);
        }
    }
}
