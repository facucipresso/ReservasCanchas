using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexCardResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public ComplexState ComplexState { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public decimal LowestPricePerField { get; set; }

    }
}
