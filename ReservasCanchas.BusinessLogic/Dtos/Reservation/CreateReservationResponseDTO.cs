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
        public DateTime CreationDate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ReservationState State { get; set; }
        public ReservationType ReservationType { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? PricePaid { get; set; }
        public 
    }
}
