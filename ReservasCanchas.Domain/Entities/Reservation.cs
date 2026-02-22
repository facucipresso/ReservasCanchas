using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        //Implementado de manera parciallll
        public int UserId { get; set; }
        public int FieldId { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentType? PaymentType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; }
        public ReservationType ReservationType { get; set; }
        public string? BlockReason { get; set; } = string.Empty;
        public ReservationState ReservationState { get; set; }
        public string? VoucherPath { get; set; } = string.Empty;
        public string? CancellationReason { get; set; }


        // Propiedad de navegacion
        public User User { get; set; } = null!;
        public Field Field { get; set; } = null!;
        // Puede ser nula si el usuario que hizo la reserva no deja reseña
        public Review? Review { get; set; }

    }
}
