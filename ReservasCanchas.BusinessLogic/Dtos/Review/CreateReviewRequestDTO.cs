using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Review
{
    public class CreateReviewRequestDTO
    {
        [Required(ErrorMessage = "El commentario es obligatorio")]
        public string Comment { get; set; } = string.Empty;

    }
}
