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
    public class UserMapper
    {
        public static UserResponseDTO ToUsusarioResponseDTO(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
        }

        public static User ToUser(UserRequestDTO userRequest)
        {
            return new User 
            {
                Name = userRequest.Name,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.Phone, 
                Status = UserStatus.Activo
                // Faltaria agregar el rol
            };
        }
    }
}
