using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string ServiceDescription { get; set; } = string.Empty;
        public bool Active { get; set; }

        //Relacion muchos a muchos con Complex
        public ICollection<Complejo> Complexes { get; set; } = new List<Complejo>();
    }
}
