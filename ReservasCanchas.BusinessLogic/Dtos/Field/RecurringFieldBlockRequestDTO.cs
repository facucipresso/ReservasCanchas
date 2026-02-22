using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class RecurringFieldBlockRequestDTO
    {
        [Required(ErrorMessage = "El día de la semana es obligatorio")]
        public WeekDay WeekDay { get; set; }
        [Required(ErrorMessage = "El horario de inicio del bloqueo recurrente es obligatorio")]
        public TimeOnly StartTime { get; set; }
        [Required(ErrorMessage = "El horario de fin del bloqueo recurrente es obligatorio")]
        public TimeOnly EndTime { get; set; }
        [Required(ErrorMessage = "La razón del bloqueo recurrente es obligatoria")]
        public string Reason { get; set; } = string.Empty;
    }
}
