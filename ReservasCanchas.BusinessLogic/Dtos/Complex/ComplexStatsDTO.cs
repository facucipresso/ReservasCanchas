namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexStatsDTO
    {
        public int totalPossibleSlots { get; set; }
        public int occupiedSlots { get; set; }
        public int matches { get; set; }
        public int specificBlocks { get; set; }
        public decimal totalRevenue { get; set; }
    }
}
