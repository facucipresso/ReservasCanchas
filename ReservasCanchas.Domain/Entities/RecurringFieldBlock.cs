using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.Domain.Entities
{
    public class RecurringFieldBlock
    {
        [Key]
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Reason {  get; set; } = string.Empty;

        //Propiedad de navegacion
        public Field Field { get; set; } = null!;
    }
}
