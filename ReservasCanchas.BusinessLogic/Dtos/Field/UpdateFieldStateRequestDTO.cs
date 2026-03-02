using ReservasCanchas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class UpdateFieldStateRequestDTO
    {
        [Required(ErrorMessage = "El estado de la cancha es obligatorio")]
        public FieldState FieldState { get; set; }
    }
}
