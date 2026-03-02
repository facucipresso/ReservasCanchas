using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
