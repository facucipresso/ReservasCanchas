using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Models.Service;
using WinFormsApp1.Models.TimeSlots;

namespace WinFormsApp1.Models.Complex
{
    public class ComplexDetailResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int PercentageSign { get; set; }
        public TimeOnly StartIllumination { get; set; }
        public int AditionalIllumination { get; set; }
        public double? AverageRating { get; set; }
        public string CBU { get; set; }
        //toma el estado como un string, no como un enum
        public string ComplexState { get; set; }
        public ICollection<ServiceResponseDTO> Services { get; set; } = new List<ServiceResponseDTO>();
        public ICollection<TimeSlotComplexResponseDTO> TimeSlots { get; set; } = new List<TimeSlotComplexResponseDTO>();
    }
}
