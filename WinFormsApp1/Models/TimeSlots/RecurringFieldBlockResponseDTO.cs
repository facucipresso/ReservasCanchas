using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Models.TimeSlots
{
    public class RecurringFieldBlockResponseDTO
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
