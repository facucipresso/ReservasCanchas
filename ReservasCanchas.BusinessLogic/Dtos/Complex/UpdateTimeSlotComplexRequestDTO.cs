using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class UpdateTimeSlotComplexRequestDTO
    {
        [Length(7, 7, ErrorMessage = "Se debe especificar una franja horaria por día")]
        public List<UpdateTimeSlotComplexDTO> TimeSlots { get; set; }
    }
}
