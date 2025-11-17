using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationForUserDTO
    {
        public int ReservationId { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public ReservationState State { get; set; }

        public string ComplexName { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;

        public int TotalPrice { get; set; }
        public int PricePaid { get; set; }

        public bool CanReview { get; set; }
    }
}
