using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Usuario
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
