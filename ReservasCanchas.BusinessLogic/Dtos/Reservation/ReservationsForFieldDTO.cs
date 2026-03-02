namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationsForFieldDTO
    {
        public int FieldId { get; set; }

        public List<TimeOnly> ReservedHours { get; set; } = new();
    }
}
