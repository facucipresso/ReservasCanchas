using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexFiltersRequestDTO
    {
        [Required(ErrorMessage = "La provincia es obligatoria")]
        public string Province { get; set; } = string.Empty;
        [Required(ErrorMessage = "La localidad es obligatoria")]
        public string Locality { get; set; } = string.Empty;
        [Required(ErrorMessage = "El dia es obligatorio")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "La hora es obligatoria")]
        public TimeOnly Hour { get; set; }
        [Required(ErrorMessage = "La tipo de cancha es obligatorio")]
        public FieldType FieldType { get; set; }
    }
}
