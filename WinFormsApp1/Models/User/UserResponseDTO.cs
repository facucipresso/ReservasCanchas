using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Enum;

namespace WinFormsApp1.Models.User
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        //public string Name { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }
        // Ver si devolvemos complejos, reservas o reseñas
    }
}
