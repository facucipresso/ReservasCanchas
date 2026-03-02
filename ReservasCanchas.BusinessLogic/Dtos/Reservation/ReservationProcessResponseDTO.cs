namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationProcessResponseDTO
    {
        public bool ExistReservationProcessForUser { get; set; }
        public string ReservationProcessId { get; set; } = string.Empty;    
    }
}
