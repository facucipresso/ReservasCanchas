using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Enum;

namespace WinFormsApp1.Models.User
{
    public class UserResponseWithRoleDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
