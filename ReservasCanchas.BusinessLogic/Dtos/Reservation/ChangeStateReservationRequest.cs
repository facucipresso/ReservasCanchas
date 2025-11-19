using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ChangeStateReservationRequest
    {
        public int ReservationId { get; set; }
        public int FieldId { get; set; }
        public ReservationState ReservationState { get; set; }
    }
}
