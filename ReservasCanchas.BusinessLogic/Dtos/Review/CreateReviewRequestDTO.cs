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
        [Required(ErrorMessage = "El id de la reserva es obligatorio")]
        public int ReservationId { get; set; }
        public string Comment { get; set; } = string.Empty;
        [Required(ErrorMessage = "La calificación es obligatoria")]
        public int Score { get; set; }
    }
}
