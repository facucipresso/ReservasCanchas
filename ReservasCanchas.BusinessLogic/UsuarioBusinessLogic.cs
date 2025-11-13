using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
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

        public async Task<UsuarioResponseDTO?> GetById(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException("Usuario con id " + id + " no encontrado");
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
            if (await _userRepo.ExistUserAsync(usuarioRequest.Name, usuarioRequest.LastName, usuarioRequest.Email))
            {
                throw new BadRequestException("Ya existe un usuario con los datos ingresados: " + usuarioRequest.Name+" , "+ usuarioRequest.LastName + " y " + usuarioRequest.Email);
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

            if (await _userRepo.ExistUserAsync(userDTO.Name, userDTO.LastName, userDTO.Email))
            {
                throw new BadRequestException("Ya existe un usuario con los datos ingresados: " + userDTO.Name + " , " + userDTO.LastName + " y " + userDTO.Email);
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
    }
}
