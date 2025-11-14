using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Enums
{
    public enum ReservationState
    {
        Pendiente = 0,
        Aprobada = 1,
        CanceladoConDevolucion = 2,
        CanceladoSinDevolucion = 3,
        Completada = 4
    }
}
