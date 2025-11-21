using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    //sacar este metodo, que venga el date en una consuta
    public class ReservationForDayRequest
    {
        public DateOnly Date {  get; set; }
        public FieldType FieldType { get; set; }
        public FloorType FloorType { get; set; }
    }
}
