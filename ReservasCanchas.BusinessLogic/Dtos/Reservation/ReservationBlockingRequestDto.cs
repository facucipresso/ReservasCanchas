using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationBlockingRequestDto
    {
        public int FieldId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public string BlockReason { get; set; } = string.Empty;

    }
}
