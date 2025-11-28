using Microsoft.AspNetCore.Identity;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class UserBusinessLogic
    {
        private readonly UserRepository _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AuthService _authService;
        public UserBusinessLogic(UserRepository userRepo, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, AuthService authService)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }

            var usuarioDto = UserMapper.ToUsusarioResponseDTO(user);
            return usuarioDto;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();

            var usersDto = users
                .Select(UserMapper.ToUsusarioResponseDTO)
                .ToList();

            return usersDto;
        }

        // Esto lo voy a sacar porque no quiero que se cree un usuario por otro lado que no sea el account controller
        /*
        public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO usuarioRequest)
        {
            if ((await _userRepo.ExistByPhoneAsync(usuarioRequest.Phone) && await _userRepo.ExistByEmailAsync(usuarioRequest.Email)))
            {
                throw new BadRequestException("Ya existe un usuario con ese email y telefono ingresados: " + usuarioRequest.Email + " , " + usuarioRequest.Phone);
            }
            if (await _userRepo.ExistByEmailAsync(usuarioRequest.Email))
            {
                throw new BadRequestException("Ya existe un usuario con ese email ingresados: "+ usuarioRequest.Email);
            }
            if(await _userRepo.ExistByPhoneAsync(usuarioRequest.Phone))
            {
                throw new BadRequestException("Ya existe un usuario con ese telefono ingresados: " + usuarioRequest.Phone);
            }
            

            var usuario = UserMapper.ToUser(usuarioRequest);
            var usuario2 = await _userRepo.CreateUserAsync(usuario);
            return UserMapper.ToUsusarioResponseDTO(usuario2);
        }
        */

        public async Task<UserResponseDTO> UpdateUserAsync(int id, UserRequestDTO userDTO)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }
            // Validaciones excluyendo al usuario actual
            var userEmailOwner = await _userManager.FindByEmailAsync(userDTO.Email);
            if (userEmailOwner != null && userEmailOwner.Id != id)
                throw new BadRequestException($"Ya existe un usuario con el email {userDTO.Email}");

            if (await _userRepo.ExistByPhoneExceptIdAsync(userDTO.Phone, id))
                throw new BadRequestException($"Ya existe un usuario con el teléfono {userDTO.Phone}");


            user.Name = userDTO.Name;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.PhoneNumber = userDTO.Phone;

            // Update con Identity
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException($"No se pudo actualizar correctamente el usuario {user.UserName}");


            //await _userRepo.SaveAsync();
            return UserMapper.ToUsusarioResponseDTO(user);
        }

        public async Task<User> UpdateUserRolAsync(int userId, string newRol)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null)
                throw new NotFoundException($"No se encontró el usuario con id {userId}");

            if(!await _roleManager.RoleExistsAsync(newRol))
                throw new BadRequestException($"El rol {newRol} no existe");

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, newRol);

            return user;
        }

        public async Task BlockUserAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Usuario con id " + id + " no encontrado");

            user.Status = UserStatus.Bloqueado;
            await _userRepo.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            int authenticatedUserId = _authService.GetUserId();// puse el 3 porque queria probar eliminar el user con id 3

            var user = await GetUserOrThrow(id);

            if(authenticatedUserId != id)
                throw new BadRequestException($"Solo el usuario dueño del perfil puede eliminarlo");

            if (await _userRepo.HasActiveReservationsAsync(authenticatedUserId))
                throw new BadRequestException($"No puedes eliminar tu perfil porque tienes reservas activas");

            user.Active = false;

            await _userRepo.SaveAsync();
        }

        public async Task<User> GetUserOrThrow(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"Usuario no encontrado con id {id}");

            return user;
        }
        public async Task ValidateUserState(User user)
        {
            if (user.Status == UserStatus.Bloqueado)
                throw new BadRequestException($"El usuario con id {user.Id} esta bloqueado");
        }

        public async Task<int> GetUserIdByUserRolOrThrow(string rol)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(rol);

            if (usersInRole == null || usersInRole.Count == 0)
                throw new NotFoundException($"No existe ningún usuario con el rol {rol}");

            // si solo existe un SuperAdmin (como va a pasar) → devolvemos ese
            return usersInRole.First().Id;
        }
    }
}
