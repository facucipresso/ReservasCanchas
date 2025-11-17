using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationsForUserResponseDTO
    {
        public List<ReservationForUserDTO> Upcoming { get; set; } = new List<ReservationForUserDTO>();
        public List<ReservationForUserDTO> History { get; set; } = new List<ReservationForUserDTO>();
    }
}
