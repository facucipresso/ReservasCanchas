using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Models.Reservation
{
    public class ReservationResponseDTO
    {
        public int ReservationId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public string ReservationState { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? UserLastName { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? PayType { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? PricePaid { get; set; }
        public string ReservationType { get; set; }
        public string? VoucherPath { get; set; }
    }
}
