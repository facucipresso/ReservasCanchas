using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Reservation
{
    //sacar este metodo, que venga el date en una consuta
    public class ReservationsForDayRequestDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Date {  get; set; }
        [Required(ErrorMessage = "El ComplexId es obligatorio")]
        public int complexId { get; set; }
    }
}
