using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class UpdateTimeSlotComplexDTO
    {
        [Required(ErrorMessage = "Los time slots del complejo es obligatoria")]

        public WeekDay WeekDay { get; set; }
        [Required(ErrorMessage = "Los time slots del complejo es obligatoria")]

        public TimeOnly StartTime { get; set; }
        [Required(ErrorMessage = "Los time slots del complejo es obligatoria")]

        public TimeOnly EndTime { get; set; }
    }
}
