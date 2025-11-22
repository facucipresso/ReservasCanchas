using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    public class ReservationsForFieldDTO
    {
        public int FieldId { get; set; }

        // todos los horarios reservados/bloqueados en ese día, de 1h cada uno
        public List<TimeOnly> ReservedHours { get; set; } = new();
    }
}
