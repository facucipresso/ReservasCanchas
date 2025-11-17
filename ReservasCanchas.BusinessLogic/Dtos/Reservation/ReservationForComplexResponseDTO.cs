using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationForComplexResponseDTO
    {
        public int ReservationId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public ReservationState State { get; set; }

        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public PayType PayType { get; set; }
        public int TotalPrice { get; set; }
        public int PricePaid { get; set; }
    }
}
