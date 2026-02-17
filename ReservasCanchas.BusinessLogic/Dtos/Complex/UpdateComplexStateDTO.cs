using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
     public class UpdateComplexStateDTO
    {
        [Required(ErrorMessage ="El estado del complejo es obligatorio")]
        public ComplexState State { get; set; }
        public string? CancelationReason { get; set; }
    }
}
