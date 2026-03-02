using ReservasCanchas.BusinessLogic.Dtos.User;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;

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
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserState = user.UserState
              
            };
        }

        public static UserResponseWithRoleDTO ToUsusarioWithRoleResponseDTO(User user, string role)
        {
            return new UserResponseWithRoleDTO
            {
                Id = user.Id,
                FullName = $"{user.LastName} {user.Name}",
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserState = user.UserState,
                Role = role
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
                UserState = UserState.Activo
            };
        }
    }
}
