using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class DayRecurringBlockDTO
    {
        public int RecurringBlockId { get; set; }
        public int FieldId { get; set; }

        public TimeOnly InitTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
