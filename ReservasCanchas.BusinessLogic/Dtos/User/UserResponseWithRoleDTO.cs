using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Dtos.User
{
    public class UserResponseWithRoleDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserState UserState { get; set; }
        public string Role {  get; set; } = string.Empty;
    }
}
