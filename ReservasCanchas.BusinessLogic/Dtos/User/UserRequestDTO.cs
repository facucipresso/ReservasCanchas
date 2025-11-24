using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Usuario
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El apellido del usuario es obligatorio")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email del usuario es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El telefono del usuario es obligatorio")]
        public string Phone { get; set; } = string.Empty;
    }
}
