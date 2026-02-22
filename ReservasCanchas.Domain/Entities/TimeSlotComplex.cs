using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class TimeSlotComplex
    {
        [Key]
        public int Id { get; set; }
        public int ComplexId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Propiedad de navegacion
        public Complex Complex {  get; set; } = null!;
    }
}
