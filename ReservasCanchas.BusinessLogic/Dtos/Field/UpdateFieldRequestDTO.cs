using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class UpdateFieldRequestDTO
    {
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio por hora no puede ser negativo")]
        public decimal? HourPrice { get; set; }
        public bool? Ilumination { get; set; }
        public bool? Covered { get;set; }
    }
}
