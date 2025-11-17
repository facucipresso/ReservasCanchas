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
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Usuario
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Usuario
                .Where(u => u.Status == UserStatus.Activo)
                .ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await _context.Usuario.AnyAsync(s =>  s.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistByPhoneAsync(string phone)
        {
            return await _context.Usuario.AnyAsync(u => u.Phone.ToLower() == phone.ToLower());
        }
    }
}
