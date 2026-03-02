using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class UpdateTimeSlotComplexRequestDTO
    {
        [Required(ErrorMessage = "La lista de franjas horarias es obligatoria")]
        [Length(7, 7, ErrorMessage = "Se debe especificar una franja horaria por día")]
        public List<TimeSlotComplexRequestDTO> TimeSlots { get; set; } = new List<TimeSlotComplexRequestDTO>();
    }
}
