using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.Domain.Entities
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string ServiceDescription { get; set; } = string.Empty;
        public bool Active { get; set; }

        //Relacion muchos a muchos con Complex
        public ICollection<Complex> Complexes { get; set; } = new List<Complex>();
    }
}
