using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ChangeStateReservationRequestDTO
    {
        [Required(ErrorMessage = "El nuevo estado es obligatorio")]
        public ReservationState newState { get; set; }
        public string? CancelationReason { get; set; }
    }
}
