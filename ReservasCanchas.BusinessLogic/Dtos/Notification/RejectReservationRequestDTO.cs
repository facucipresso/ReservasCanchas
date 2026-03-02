namespace ReservasCanchas.BusinessLogic.Dtos.Notification
{
    public class RejectReservationRequestDTO
    {
        public int ReservationId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
