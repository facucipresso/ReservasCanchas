using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
{
    public class UpdateComplexBasicInfoRequestDTO
    {
        [Required(ErrorMessage = "El nombre del complejo es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción del complejo es obligatoria")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "La provincia del complejo es obligatoria")]
        public string Province { get; set; } = string.Empty;
        [Required(ErrorMessage = "La localidad del complejo es obligatoria")]
        public string Locality { get; set; } = string.Empty;
        [Required(ErrorMessage = "La calle del complejo es obligatoria")]
        public string Street { get; set; } = string.Empty;
        [Required(ErrorMessage = "La altura del complejo es obligatorio")]
        public string Number { get; set; } = string.Empty;
        [Required(ErrorMessage = "El teléfono del complejo es obligatorio")]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage ="El CBU del complejo es obligatorio")]
        [Length(22,22,ErrorMessage ="El CBU debe tener 22 caracteres")]
        public string CBU { get; set; } = string.Empty;
        [Required(ErrorMessage = "El porcentaje de seña es obligatorio")]
        [Range(0, 100, ErrorMessage = "El porcentaje de seña debe estar entre 0 y 100")]
        public int PercentageSign { get; set; }
        [Required(ErrorMessage = "El horario de inicio de iluminación es obligatorio")]
        public TimeOnly StartIlumination { get; set; }
        [Required(ErrorMessage = "El porcentaje de aumento por iluminación es obligatorio")]
        [Range(0, 100, ErrorMessage = "El porcentaje de aumento por iluminación debe estar entre 0 y 100")]
        public int AditionalIlumination { get; set; }

    }
}