using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class RecurringFieldBlockResponseDTO
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly InitHour { get; set; }
        public TimeOnly EndHour { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
