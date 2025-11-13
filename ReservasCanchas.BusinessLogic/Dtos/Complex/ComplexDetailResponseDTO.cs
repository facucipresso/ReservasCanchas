using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.BusinessLogic.Dtos.Service;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Complex
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
        public TimeOnly StartIlumination { get; set; }
        public int AditionalIlumination { get; set; }
        public ComplexState State { get; set; }
        public ICollection<ServiceResponseDTO> Services { get; set; } = new List<ServiceResponseDTO>();
        public ICollection<FieldResponseDTO> Fields { get; set; } = new List<FieldResponseDTO>();
        public ICollection<TimeSlotResponseDTO> TimeSlots { get; set; } = new List<TimeSlotResponseDTO>();
    }
}
