using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class CreateReservationRequestDTO
    {
        public int FieldId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly InitTime { get; set; }
        public PayType PayType { get; set; }
        public int PricePaid { get; set; }
    }
}
