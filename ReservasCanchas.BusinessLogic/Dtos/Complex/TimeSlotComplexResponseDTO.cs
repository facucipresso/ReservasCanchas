using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class TimeSlotComplexResponseDTO
    {
        public int Id { get; set; }
        public int ComplexId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly InitTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
