using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class DayAvailabilityResponseDTO
    {
        public int ComplexId { get; set; }
        public DateOnly Date { get; set; }

        public List<DayReservationDTO> Reservations { get; set; } = new();
        public List<DayRecurringBlockDTO> RecurringBlocks { get; set; } = new();
    }
}
