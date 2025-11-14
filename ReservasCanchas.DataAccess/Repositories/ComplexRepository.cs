using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            _context.Complejo.Add(complex);
            await _context.SaveChangesAsync();
            return complex;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string complexName)
        {
            return await _context.Complejo.AnyAsync(c => c.Name.ToLower() == complexName.ToLower() && c.Active);
        }

        public async Task<bool> ExistsByAddressAsync(string street, string number, string locality, string province)
        {
            return await _context.Complejo.AnyAsync(c => c.Street.ToLower() == street.ToLower() &&
                                                        c.Number.ToLower() == number.ToLower() &&
                                                        c.Locality.ToLower() == locality.ToLower() &&
                                                        c.Province.ToLower() == province.ToLower() &&
                                                        c.Active);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await _context.Complejo.AnyAsync(c => c.Phone.ToLower() == phone.ToLower() && c.Active);
        }

        public async Task<List<Complejo>> GetComplexesForSearchAsync(string locality)
        {
            return await _context.Complejo
                .Include(c => c.TimeSlots)
                .Include(c => c.Fields)
                    .ThenInclude(f => f.Reservations)
                .Include(c => c.Fields)
                    .ThenInclude(f => f.recurringCourtBlocks)
                .Where(c =>
                    c.Locality == locality &&
                    c.Active &&
                    c.State == ComplexState.Habilitado
                )
                .ToListAsync();
        }

    }
}
