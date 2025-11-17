using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class UsuarioMapper
    {
        public static UsuarioResponseDTO ToUsusarioResponseDTO(User user)
        {
            return new UsuarioResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        public static User ToUsuario(UsuarioRequestDTO usuarioRequest)
        {
            return new User 
            {
                Name = usuarioRequest.Name,
                LastName = usuarioRequest.LastName,
                Email = usuarioRequest.Email,
                Phone = usuarioRequest.Phone,
                Status = UserStatus.Activo
                // Faltaria agregar el rol
            };
        }
    }
}
