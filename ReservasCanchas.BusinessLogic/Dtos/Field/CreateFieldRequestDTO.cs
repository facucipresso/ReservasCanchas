using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class CreateFieldRequestDTO
    {
        [Required(ErrorMessage = "El Id del complejo es obligatorio")]
        public int ComplexId { get; set; }
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
        public bool Ilumination { get; set; }
        [Required(ErrorMessage = "El cerramiento de la cancha es obligatorio")]
        public bool Covered { get; set; }

        //[Length(7, 7, ErrorMessage = "Se debe especificar una franja horaria por día")] lo comento para probar con swagger
        [MinLength(7, ErrorMessage = "Debe haber 7 días")]
        [MaxLength(7, ErrorMessage = "Debe haber 7 días")]
        public ICollection<TimeSlotFieldRequestDTO> TimeSlotsField { get; set; } = new List<TimeSlotFieldRequestDTO>();
    }
}
