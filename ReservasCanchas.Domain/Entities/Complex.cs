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
    public class Complejo
    {
        [Key]
        public int Id { get; set; }
        // Todavia no armamos nada para usuario
        //public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int PercentageSign { get; set; }
        public TimeOnly StartIlumination { get; set; }
        public int AditionalIlumination { get; set; }
        public ComplexState Estado { get; set; }
        public bool Active { get; set; }

        // Relacion muchos a muchos con Service
        public ICollection<Service> Services { get; set; } = new List<Service>();

        // Relacion 1 a muchos con canchas
        // En relaciones 1 a n no puedo inicializar la propiedad de navegacion
        public ICollection<Field> Fields { get; set; } = null!;

        //Referencia 1 a 1 con TimeSlotComplex, esto me permitiria hacer t
        public TimeSlotComplex TimeSlotComplex { get; set; } = new TimeSlotComplex();

    }
}
