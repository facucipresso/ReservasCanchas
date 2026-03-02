using System.ComponentModel.DataAnnotations;

namespace ReservasCanchas.BusinessLogic.Dtos.Service
{
    public class ServiceRequestDTO
    {
        [Required(ErrorMessage ="La descripción del servicio es obligatoria")]
        public string ServiceDescription { get; set; } = string.Empty;

    }
}
