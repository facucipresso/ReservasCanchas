using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public Rol Rol { get; set; }

        public bool Active { get; set; }


        // Relacion con complejos
        public ICollection<Complex> Complejos { get; set; } = new List<Complex>();
        // Relacion con reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // Relacion con reseñas
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
