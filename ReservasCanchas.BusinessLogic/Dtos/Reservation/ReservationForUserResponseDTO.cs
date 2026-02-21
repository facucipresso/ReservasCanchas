using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationForUserResponseDTO
    {
        public int ReservationId { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public ReservationState ReservationState { get; set; }

        public string ComplexName { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;

        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; } 

        public bool CanReview { get; set; }
    }
}
