using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;


namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexFiltersRequestDTO
    {
        [Required(ErrorMessage = "La provincia es obligatoria")]
        public string Province { get; set; } = string.Empty;
        public string? Locality { get; set; } = string.Empty;
        [Required(ErrorMessage = "El dia es obligatorio")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "La hora es obligatoria")]
        public TimeOnly Hour { get; set; }
        [Required(ErrorMessage = "La tipo de cancha es obligatorio")]
        public FieldType FieldType { get; set; }
    }
}
