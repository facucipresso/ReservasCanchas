using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationResponseDTO
    {
        public int ReservationId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public ReservationState ReservationState { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; } = string.Empty; 
        public string? UserLastName { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public PayType? PayType { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? PricePaid { get; set; }
        public ReservationType ReservationType { get; set; }
        public string? VoucherPath { get; set; }
    }
}
