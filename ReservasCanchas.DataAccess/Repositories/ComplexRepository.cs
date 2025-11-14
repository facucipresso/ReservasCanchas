using ReservasCanchas.DataAccess.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservasCanchas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class ComplexRepository
    {
        private readonly AppDbContext _context;
        public ComplexRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Complejo> GetComplexByIdAsync(int id)
        {
            return null;
        }

        public async Task<Complejo> GetComplexByIdWithRelationsAsync(int id)
        {
            return null;
        }

        public async Task<List<Complejo>> GetAllComplexesAsync()
        {
            return null;
        }

        public async Task<List<Complejo>> GetComplexesByUserIdAsync(int userId)
        {
            return await _context.Complejo
                         .Where(c => c.UserId == userId && c.Active)
                         .ToListAsync();
        }

        public async Task<Complejo> CreateComplexAsync(Complejo complex)
        {
            return null;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
