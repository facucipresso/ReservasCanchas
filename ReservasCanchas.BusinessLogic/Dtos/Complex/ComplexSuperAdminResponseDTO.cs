using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class ComplexSuperAdminResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NameUser { get; set; } = string.Empty;
        public string LastNameUser { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ComplexState ComplexState { get; set; }
    }
}
