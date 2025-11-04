using System.ComponentModel.DataAnnotations;

namespace Demo2.Models
{
    public class Complex
    {
        [Key]
        public int Id { get; set; }
        //public int IdUser { get; set; }
        public string ComplexName { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageRoute { get; set; } = string.Empty;
    }
}
