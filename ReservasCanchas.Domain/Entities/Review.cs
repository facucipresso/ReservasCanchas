using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int IdUsuario { get; set; }
        public string Comment { get; set; } = string.Empty;

        // Propiedad de navegacion
        public Usuario Usuario { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
        
    }
}
