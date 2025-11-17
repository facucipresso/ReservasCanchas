using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class UpdateTimeSlotFieldRequestDTO
    {
        [Required(ErrorMessage = "La lista de franjas horarias es obligatoria")]
        [Length(7, 7, ErrorMessage = "Se debe especificar una franja horaria por día")]
        public List<TimeSlotFieldRequestDTO> TimeSlotsField { get; set; } = new List<TimeSlotFieldRequestDTO>();
    }
}
