using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class RecurringFieldBlockResponseDTO
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
