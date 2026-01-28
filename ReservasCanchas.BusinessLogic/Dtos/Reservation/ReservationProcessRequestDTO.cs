using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationProcessRequestDTO
    {
        [Required(ErrorMessage = "El id del complejo es obligatorio")]
        public int ComplexId { get; set; }
        [Required(ErrorMessage = "El id de la cancha es obligatorio")]
        public int FieldId { get; set; }
        [Required(ErrorMessage = "La fecha de la reserva es obligatoria")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "El horario de inicio de la reserva es obligatorio")]
        public TimeOnly StartTime { get; set; }
    }
}
