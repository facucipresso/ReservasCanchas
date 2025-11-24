using Microsoft.EntityFrameworkCore;
using ReservasCanchas.DataAccess.Persistance;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.User
                .Where(u => u.Id == id && u.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User
                .Where(u => u.Active)
                .ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await _context.User.AnyAsync(s =>  s.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistByPhoneAsync(string phone)
        {
            return await _context.User.AnyAsync(u => u.Phone.ToLower() == phone.ToLower());
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> HasActiveReservationsAsync(int id)
        {
            return await _context.User
                .Where(u => u.Id == id && u.Active)
                .SelectMany(u => u.Reservations)
                .AnyAsync(r => r.ReservationState == ReservationState.Aprobada);
        }
    }
}
