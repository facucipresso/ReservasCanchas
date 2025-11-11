using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class RecurringFieldBlock
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly InitHour { get; set; }
        public TimeOnly EndHour { get; set; }
        public string Reason {  get; set; } = string.Empty;

        //Propiedad de navegacion
        public Field Field { get; set; } = null!;
    }
}
