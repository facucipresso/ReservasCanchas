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
        public async Task<Usuario?> GetUserByIdAsync(int id)
        {
            // Ver esto porque creo que el unico que podria ver todos los usurios es el superadmin y capaz quiere ver todos
            return await _context.Usuario
                .Where(s => s.Id == id && s.Status == UserStatus.Activo)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> GetAllUsersAsync()
        {
            return await _context.Usuario
                .Where(u => u.Status == UserStatus.Activo)
                .ToListAsync();
        }

        public async Task<Usuario> CreateUserAsync(Usuario user)
        {
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario> UpdateUserAsync(Usuario user)
        {
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<bool> ExistUserAsync(string name, string lastName, string email)
        {
            // Que pasa si el usuario existe pero esta bloqueado?
            return await _context.Usuario.AnyAsync(s => s.Name.ToLower() == name.ToLower() && s.LastName.ToLower() == lastName.ToLower() && s.Email.ToLower() == email.ToLower() && s.Status == UserStatus.Activo);
        }
    }
}
