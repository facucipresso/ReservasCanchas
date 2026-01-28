using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Models.TimeSlots;

namespace WinFormsApp1.Models.Field
{
    public class FieldDetailResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ComplexId { get; set; }
        //public FieldType FieldType { get; set; }
        public string FieldType { get; set; }
        //public FloorType FloorType { get; set; }
        public string FloorType { get; set; }
        public decimal HourPrice { get; set; }
        public bool Ilumination { get; set; }
        public bool Covered { get; set; }
        public bool Active { get; set; }
        //public FieldState FieldState { get; set; }
        public string FieldState { get; set; }

        public ICollection<TimeSlotFieldResponseDTO> TimeSlotsField { get; set; } = new List<TimeSlotFieldResponseDTO>();
        public ICollection<RecurringFieldBlockResponseDTO> RecurringCourtBlocks { get; set; } = new List<RecurringFieldBlockResponseDTO>();
    }
}