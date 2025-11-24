using Microsoft.AspNetCore.Http;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class CreateReservationRequestDTO
    {
        [Required(ErrorMessage = "El id de la cancha es obligatorio")]
        public int FieldId { get; set; }
        [Required(ErrorMessage = "La fecha de la reserva es obligatorio")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "El horario de inicio de la reserva es obligatorio")]
        public TimeOnly InitTime { get; set; }
        [Required(ErrorMessage = "El tipo de reserva es obligatorio")]
        public ReservationType ReservationType { get; set; }
        public PayType? PayType { get; set; }
        public int? PricePaid { get; set; }
        public IFormFile? Image { get; set; } = null!;
        public string? BlockReason { get; set; } = string.Empty;
    }
}
