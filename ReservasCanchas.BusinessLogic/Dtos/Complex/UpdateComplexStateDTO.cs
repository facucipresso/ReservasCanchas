using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
     public class UpdateComplexStateDTO
    {
        [Required(ErrorMessage ="El estado del complejo es obligatorio")]
        public ComplexState ComplexState { get; set; }
        public string? CancelationReason { get; set; }
    }
}
