using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class FieldResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ComplexId { get; set; }
        public FieldType FieldType { get; set; }
        public FloorType FloorType { get; set; }
        public decimal HourPrice { get; set; }
        public bool Ilumination { get; set; }
        public bool Covered { get; set; }
    }
}
