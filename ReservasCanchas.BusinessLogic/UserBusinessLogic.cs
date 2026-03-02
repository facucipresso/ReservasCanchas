using Microsoft.AspNetCore.Identity;
using ReservasCanchas.BusinessLogic.Dtos.User;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System.Data;


namespace ReservasCanchas.BusinessLogic
{
    public class UserBusinessLogic
    {
        private readonly UserRepository _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AuthService _authService;
        private readonly ComplexRepository _complexRepo;
        public UserBusinessLogic(UserRepository userRepo, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, AuthService authService, ComplexRepository complexRepository)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _complexRepo = complexRepository;
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

        public async Task<List<UserResponseWithRoleDTO>> GetAllUsersWithRoleAsync()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var response = new List<UserResponseWithRoleDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "Sin rol";
                response.Add(UserMapper.ToUsusarioWithRoleResponseDTO(user, role));
            }

            return response;
        }

        public async Task<List<UserResponseWithRoleDTO>> GetLastSixUsersWithRoleAsync()
        {
            var users = await _userRepo.GetLastSixUsersAsync(); 
            var response = new List<UserResponseWithRoleDTO>(); 

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "Sin rol";
                response.Add(UserMapper.ToUsusarioWithRoleResponseDTO(user, role));
            }

            return response;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _userRepo.GetTotalUsersAsync();
        }

        public async Task<UserResponseDTO> UpdateUserAsync(int id, UserRequestDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NotFoundException($"Usuario con id {id} no encontrado");

            var userEmailOwner = await _userManager.FindByEmailAsync(userDTO.Email);
            if (userEmailOwner != null && userEmailOwner.Id != id)
                throw new BadRequestException($"Ya existe un usuario con el email {userDTO.Email}");

            if (await _userRepo.ExistByPhoneExceptIdAsync(userDTO.Phone, id))
                throw new BadRequestException($"Ya existe un usuario con el teléfono {userDTO.Phone}");

            user.Name = userDTO.Name;
            user.LastName = userDTO.LastName;
            user.PhoneNumber = userDTO.Phone;

            var emailResult = await _userManager.SetEmailAsync(user, userDTO.Email);
            if (!emailResult.Succeeded)
                throw new BadRequestException(string.Join(" | ", emailResult.Errors.Select(e => e.Description)));

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new BadRequestException(string.Join(" | ", result.Errors.Select(e => e.Description)));

            return UserMapper.ToUsusarioResponseDTO(user);
        }


        public async Task<User> UpdateUserRolAsync(int userId, string newRol)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new NotFoundException($"No se encontró el usuario con id {userId}");

            if (!await _roleManager.RoleExistsAsync(newRol))
                throw new BadRequestException($"El rol {newRol} no existe");

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, newRol);

            return user;
        }


        public async Task BlockUserAsync(int id)
        {
            var user = await _userRepo.GetUserByIdTrackedAsync(id);
            if (user == null)
                throw new NotFoundException("Usuario con id " + id + " no encontrado");

            //aca me traigo sus complejos, si es que tiene, y los deshabilito tambien
            var userComplex = await _complexRepo.GetComplexesByUserIdAsync(id);
            if (userComplex != null)
            {
                foreach(var item in userComplex)
                {
                    item.ComplexState = ComplexState.Deshabilitado;
                }
                await _complexRepo.SaveAsync();
            }

            user.UserState = UserState.Bloqueado;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar el estado del usuario");
            }
        }

        public async Task UnBlockUserAsync(int id)
        {
            var user = await _userRepo.GetUserByIdTrackedAsync(id);
            if (user == null)
                throw new NotFoundException("Usuario con id " + id + " no encontrado");

            //me traigo sus complejos, si es que tiene, y los pongo en pendiente
            var userComplex = await _complexRepo.GetComplexesByUserIdAsync(id);
            if (userComplex != null)
            {
                foreach (var item in userComplex)
                {
                    item.ComplexState = ComplexState.Pendiente;
                }
                await _complexRepo.SaveAsync();
            }

            user.UserState = UserState.Activo;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("No se pudo actualizar el estado del usuario");
            }

        }

        public async Task DeleteUserAsync(int id)
        {
            int authenticatedUserId = _authService.GetUserId();

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
            if (user.UserState == UserState.Bloqueado)
                throw new BadRequestException($"El usuario con id {user.Id} esta bloqueado");
        }

        public async Task<int> GetUserIdByUserRolOrThrow(string rol)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(rol);

            if (usersInRole == null || usersInRole.Count == 0)
                throw new NotFoundException($"No existe ningún usuario con el rol {rol}");

            return usersInRole.First().Id;
        }


    }
}
