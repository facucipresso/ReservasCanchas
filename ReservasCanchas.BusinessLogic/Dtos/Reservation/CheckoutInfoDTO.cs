using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class CheckoutInfoDTO
    {
        public int UserId { get; set; }
        public int ComplexId { get; set; }
        public int FieldId { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public bool Illumination { get; set; }
    }
}
