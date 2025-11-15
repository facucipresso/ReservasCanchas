using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class FieldRepository
    {
        private readonly AppDbContext _context;
        public FieldRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Field?> GetFieldByIdAsync(int id)
        {
            return await _context.Field.FirstOrDefaultAsync(f => f.Id == id && f.Active);
        }

        public Task<Field?> GetFieldByIdWithBlocksAsync(int id)
        {
            return _context.Field
                .Include(f => f.RecurringCourtBlocks)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Field>> GetAllFieldsAsync()
        {
            return await _context.Field
                .Where(f => f.Active)
                .ToListAsync();
        }

        public async Task<List<Field>> GetFieldsByComplexIdAsync(int complexId)
        {
            return await _context.Field
                .Where(f => f.ComplexId == complexId && f.Active)
                .ToListAsync();
        }

        public async Task<Field> CreateFieldAsync(Field field)
        {
            _context.Field.Add(field);
            await _context.SaveChangesAsync();
            return field;
        }

        public async Task<int> CountFieldsByComplexAsync(int complexId)
        {
            return await _context.Field
                .Where(f => f.ComplexId == complexId && f.Active)
                .CountAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
