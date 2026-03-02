using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationDetailResponseDTO
    {
        public int ReservationId { get; set; }

        // Contexto
        public bool IsAdmin { get; set; }
        public ReservationState ReservationState { get; set; }
        public ReservationType ReservationType { get; set; }

        // Fecha y hora
        public DateTime CreatedAt { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }

        // Pago
        public PaymentType? PaymentType { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }

        // Iluminación
        public bool HasFieldIllumination { get; set; }
        public bool HasIllumination { get; set; }
        public decimal IlluminationAmount { get; set; }

        // Comprobante
        public string? VoucherUrl { get; set; }

        // Usuario
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;

        // Cancha
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string FloorType { get; set; } = string.Empty;
        public decimal HourPrice { get; set; }

        // Complejo
        public int ComplexId { get; set; }
        public string ComplexName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        //Bloqueo

        public string BlockReason { get; set; } = string.Empty;

        // Review

        public bool hasReview { get; set; }
    }
}
