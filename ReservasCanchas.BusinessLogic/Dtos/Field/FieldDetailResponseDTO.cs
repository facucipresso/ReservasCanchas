using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Field
{
    public class FieldDetailResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ComplexId { get; set; }
        public FieldType FieldType { get; set; }
        public FloorType FloorType { get; set; }
        public decimal HourPrice { get; set; }
        public bool Illumination { get; set; }
        public bool Covered { get; set; }
        public bool Active { get; set; }
        public FieldState FieldState { get; set; }

        public ICollection<TimeSlotFieldResponseDTO> TimeSlotsField { get; set; } = new List<TimeSlotFieldResponseDTO>();
        public ICollection<RecurringFieldBlockResponseDTO> RecurringCourtBlocks { get; set; } = new List<RecurringFieldBlockResponseDTO>();
    }
}
