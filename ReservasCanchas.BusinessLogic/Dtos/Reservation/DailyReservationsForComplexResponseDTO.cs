namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class DailyReservationsForComplexResponseDTO
    {
        public int ComplexId { get; set; }
        public DateOnly Date { get; set; }

        public List<ReservationsForFieldDTO> FieldsWithReservedHours { get; set; } = new (); // ese dto tiene el fieldId y la lista de horas reservadas para esa cancha ese dia
    }
}
