using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationProcessResponseDTO
    {
        public bool ExistReservationProcessForUser { get; set; }
        public string ReservationProcessId { get; set; } = string.Empty;    
    }
}
