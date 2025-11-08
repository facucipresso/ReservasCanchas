using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class TimeSlotField
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly InitTime { get; set; }
        public TimeOnly EndTime { get; set; }

        // Propiedad de navegacion
        public Field Field { get; set; } = new Field();
    }
}
