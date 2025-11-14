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
        [Required(ErrorMessage = "Los time slots del complejo es obligatoria")]
        public List<UpdateTimeSlotComplexDTO> TimeSlots { get; set; }
    }
}
