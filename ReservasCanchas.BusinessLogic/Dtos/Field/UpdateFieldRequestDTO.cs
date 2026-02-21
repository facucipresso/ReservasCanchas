using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class UpdateFieldRequestDTO
    {
        [Required(ErrorMessage = "El nombre de la cancha es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El tipo de cancha es obligatorio")]
        public FieldType FieldType { get; set; }
        [Required(ErrorMessage = "El tipo de piso es obligatorio")]
        public FloorType FloorType { get; set; }
        [Required(ErrorMessage = "El precio por hora es obligatorio")]
        [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "El precio por hora no puede ser negativo")]
        public decimal HourPrice { get; set; }
        [Required(ErrorMessage = "La iluminacion de la cancha es obligatoria")]
        public bool Illumination { get; set; } 
        [Required(ErrorMessage = "El cerramiento de la cancha es obligatorio")]
        public bool Covered { get; set; }
    }
}
