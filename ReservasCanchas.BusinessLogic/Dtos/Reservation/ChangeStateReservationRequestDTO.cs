using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ChangeStateReservationRequestDTO
    {
        [Required(ErrorMessage = "El nuevo estado es obligatorio")]
        public ReservationState newState { get; set; }
        public string? CancelationReason { get; set; }
    }
}
