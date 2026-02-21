using Microsoft.AspNetCore.Identity;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    // PASO 0, paquete identity y extender de nuestra clase (paso 1 en el program.cs)
    public class User : IdentityUser <int> 
    {
        //public int Id { get; set; }                           le saco el id porque identity ya me provee el id
        //public string UserName { get; set; }                  le saco el userName porque lo maneja identity
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;     lo maneja identity, usar 'Email'
        //public string Phone { get; set; } = string.Empty;     lo maneja identity, usar 'PhoneNumber'
        public UserState UserState { get; set; }
        //public Rol Rol { get; set; }                          manejo los roles con identity
        //capaz esta prop la podriamos sacar
        public bool Active { get; set; }


        // Relacion con complejos
        public ICollection<Complex> Complejos { get; set; } = new List<Complex>();
        // Relacion con reservas
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        // Relacion con reseñas
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        // Relación con notificaciones
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    }
}
