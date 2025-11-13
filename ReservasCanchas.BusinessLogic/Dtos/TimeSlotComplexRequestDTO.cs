using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos
{
    public class TimeSlotComplexRequestDTO
    {
        [Required(ErrorMessage = "El día de la semana es obligatorio")]
        public WeekDay WeekDay { get; set; }
        [Required(ErrorMessage = "El horario de inicio de la franja horaria es obligatorio")]]
        public TimeOnly InitTime { get; set; }
        [Required(ErrorMessage = "El horario de fin de la franja horaria es obligatorio")]]
        public TimeOnly EndTime { get; set; }
    }
}
