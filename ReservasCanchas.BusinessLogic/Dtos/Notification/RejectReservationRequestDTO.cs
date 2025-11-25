using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Notification
{
    public class RejectReservationRequestDTO
    {
        public int ReservationId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
