using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.Domain.Entities
{
    public class TimeSlotField
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Propiedad de navegacion
        public Field Field { get; set; } = null!;
    }
}
