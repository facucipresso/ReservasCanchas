using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public UserRepository(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //no trackeado, mas perfo si no voy a modificar la entidad, solo mapearla o leer los datos
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                //.Where(u => u.Id == id && u.Active)
                //.Where (u => u.Id == id && u.UserState == UserState.Activo) me da error cuando el admin lo busca
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        //a las entidades que las voy a modificar, user manager las necesita trackeadas
        public async Task<User?> GetUserByIdTrackedAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Ya no existe User.Rol, identity maneja los roles aparte
        /*
        public async Task<User?> GetUserIdByRolAsync(Rol rol)
        {
            return await _context.Users
                .Where(u => u.Rol == rol)
                .FirstOrDefaultAsync();
        }
        */

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                //.Where (u => u.UserState == UserState.Activo) comento esta linea porque el admin tiene que ver todos los usuarios
                .ToListAsync();
        }

        // Ahora se usa #####UserManager.CreateAsync#####
        /*
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        */

        // Ya lo hace solo #####User.Manager.FindByEmailAsync#####
        /*
        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(s =>  s.Email.ToLower() == email.ToLower());
        }
        */

        public async Task<bool> ExistByPhoneAsync(string phone)
        {
            //return await _context.Users.AnyAsync(u => u.Phone.ToLower() == phone.ToLower());
            return await _userManager.Users.AnyAsync(u => u.PhoneNumber == phone);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> HasActiveReservationsAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id && u.Active)
                .SelectMany(u => u.Reservations)
                .AnyAsync(r => r.ReservationState == ReservationState.Aprobada);
        }

        /*   Esto hay que ver como lo hago despues
        public async Task<bool> ExistByEmailExceptIdAsync(string email, int id)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);
        }
        */

        public async Task<bool> ExistByPhoneExceptIdAsync(string phone, int id)
        {
            //return await _context.Users.AnyAsync(u => u.Phone == phone && u.Id != id);
            return await _userManager.Users.AnyAsync(u => u.PhoneNumber == phone && u.Id != id);
        }

        public async Task<int> GetTotalUsersAsync()
        {
            var totalUsers = await _context.Users
                //.Where(u => u.Active ==  true)
                .CountAsync();

            return totalUsers;
        }

        public async Task<List<User>> GetLastSixUsersAsync()
        {
            var totalUsers = _context.Users
                //.Where(u => u.Active == true)
                .OrderByDescending(e => e.Id)
                .Take(6)
                .ToList();

            return totalUsers;
        }
    }
}
