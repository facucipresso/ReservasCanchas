using System.ComponentModel.DataAnnotations;

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
