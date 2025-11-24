using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class UsuarioBusinessLogic
    {
        private readonly UsuarioRepository _userRepo;
        public UsuarioBusinessLogic(UsuarioRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UsuarioResponseDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }

            var usuarioDto = UsuarioMapper.ToUsusarioResponseDTO(user);
            return usuarioDto;
        }

        public async Task<UsuarioResponseDTO?> GetByIdIfIsEnabled(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }

            if(user.Status != UserStatus.Activo)
            {
                throw new BadRequestException("Usuario bloqueado, no puede realizar operaciones");
            }

            var usuarioDto = UsuarioMapper.ToUsusarioResponseDTO(user);
            return usuarioDto;
        }

        public async Task<List<UsuarioResponseDTO>> GetAll()
        {
            var users = await _userRepo.GetAllUsersAsync();

            var usersDto = users
                .Select(UsuarioMapper.ToUsusarioResponseDTO)
                .ToList();

            return usersDto;
        }

        public async Task<UsuarioResponseDTO> Create(UsuarioRequestDTO usuarioRequest)
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
            

            var usuario = UsuarioMapper.ToUsuario(usuarioRequest);
            await _userRepo.CreateUserAsync(usuario);
            return UsuarioMapper.ToUsusarioResponseDTO(usuario);
        }

        public async Task<UsuarioResponseDTO> Update(int id, UsuarioRequestDTO userDTO)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }
            if ((await _userRepo.ExistByPhoneAsync(userDTO.Phone) && await _userRepo.ExistByEmailAsync(userDTO.Email)))
            {
                throw new BadRequestException("Ya existe un usuario con ese email y telefono ingresados: " + userDTO.Email + " , " + userDTO.Phone);
            }
            if (await _userRepo.ExistByEmailAsync(userDTO.Email))
            {
                throw new BadRequestException("Ya existe un usuario con ese email ingresados: " + userDTO.Email);
            }
            if (await _userRepo.ExistByPhoneAsync(userDTO.Phone))
            {
                throw new BadRequestException("Ya existe un usuario con ese telefono ingresados: " + userDTO.Phone);
            }
            

            user.Name = userDTO.Name;
            user.LastName = userDTO.LastName;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;

            await _userRepo.UpdateUserAsync(user);
            return UsuarioMapper.ToUsusarioResponseDTO(user);
        }

        public async Task BlockUser(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
            }

            user.Status = UserStatus.Bloqueado;
            await _userRepo.UpdateUserAsync(user);
        }

        public async Task<User?> GetUserOrThrow(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"Usuario no encontrado con id {id}");

            if (user.Status == UserStatus.Bloqueado)
                throw new BadRequestException($"El usuario con id {id} esta bloqueado");
            return user;
        }
    }
}
