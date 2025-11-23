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
        Rechazada = 2,
        CanceladoConDevolucion = 3,
        CanceladoSinDevolucion = 4,
        Completada = 5,
        CanceladoPorAdmin = 6
    }
}
