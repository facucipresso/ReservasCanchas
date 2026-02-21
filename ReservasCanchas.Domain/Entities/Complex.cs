using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Complex
    {
        [Key]
        public int Id { get; set; }
        // Usuario tranqui
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int PercentageSign { get; set; }
        public TimeOnly StartIllumination { get; set; }
        public int AditionalIllumination { get; set; }
        public string CBU { get; set; }
        public ComplexState ComplexState { get; set; }
        public bool Active { get; set; }
        public string? CancelationReason { get; set; }

        // Propiedad de navegacion Usuario, esta es la que tiene que ser null, sino cada vez que se crea un complejo, crea un usuario nuevo
        public User User { get; set; } = null!;

        // Relacion muchos a muchos con Service
        public ICollection<Service> Services { get; set; } = new List<Service>();

        // Relacion 1 a muchos con canchas
        public ICollection<Field> Fields { get; set; } = new List<Field>();

        //Referencia 1 a 1 con TimeSlotComplex, esto me permitiria hacer t, aca tamben lo puedo inincializar
        public ICollection<TimeSlotComplex> TimeSlots { get; set; } = new List<TimeSlotComplex>(); 

    }
}
