namespace ReservasCanchas.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }

        // Propiedad de navegacion
        public User User { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
        
    }
}
