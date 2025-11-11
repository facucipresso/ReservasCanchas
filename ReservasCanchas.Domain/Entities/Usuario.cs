using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }
        //public Rol Rol { get; set; }


        // Relacion con complejos
        public ICollection<Complejo> Complejos { get; set; } = new List<Complejo>();
        // Relacion con reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // Relacion con reseñas
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
