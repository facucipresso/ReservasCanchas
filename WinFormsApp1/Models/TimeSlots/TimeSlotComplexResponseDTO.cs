using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Models.TimeSlots
{
    public class TimeSlotComplexResponseDTO
    {
        public int Id { get; set; }
        public int ComplexId { get; set; }
        //paso a string en dia de la semana
        public string WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
