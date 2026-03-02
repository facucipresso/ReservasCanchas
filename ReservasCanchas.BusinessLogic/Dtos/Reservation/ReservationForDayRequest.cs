using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationsForDayRequestDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Date {  get; set; }
        [Required(ErrorMessage = "El ComplexId es obligatorio")]
        public int complexId { get; set; }
    }
}
