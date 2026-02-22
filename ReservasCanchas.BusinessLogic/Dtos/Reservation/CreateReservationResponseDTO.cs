using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class CreateReservationResponseDTO
    {
        public int ReservationId { get; set; }
        public int FieldId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ReservationState ReservationState { get; set; }
        public ReservationType ReservationType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; } 
    }
}
