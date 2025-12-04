using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Account
{
    // PASO 6 USO DE JWT (paso 7 en applicationDbContext)

    public class RegisterDto
    {
        [Required] //que el usuario tiene que tener 4 o mas caracteres
        [MinLength(4, ErrorMessage = "El nombre de usuario debe tener al menos 4 caracteres.")]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required] // tiene que tener al menos 8 caract, ver caract especiales
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string? Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
