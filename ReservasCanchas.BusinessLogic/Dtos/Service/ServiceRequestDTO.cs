using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Service
{
    public class ServiceRequestDTO
    {
        [Required(ErrorMessage ="La descripción del servicio es obligatoria")]
        public string ServiceDescription { get; set; } = string.Empty;

    }
}
