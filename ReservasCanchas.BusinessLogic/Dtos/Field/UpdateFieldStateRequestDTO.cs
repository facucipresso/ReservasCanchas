using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class UpdateFieldStateRequestDTO
    {
        [Required(ErrorMessage = "El estado de la cancha es obligatorio")]
        public FieldState FieldState { get; set; }
    }
}
