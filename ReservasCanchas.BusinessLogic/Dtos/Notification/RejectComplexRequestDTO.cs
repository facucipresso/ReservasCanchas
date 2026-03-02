namespace ReservasCanchas.BusinessLogic.Dtos.Notification
{
    public class RejectComplexRequestDTO
    {
        public int ComplexId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
