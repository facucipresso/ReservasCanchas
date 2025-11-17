using Microsoft.VisualBasic.FileIO;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldType = ReservasCanchas.Domain.Enums.FieldType;

namespace ReservasCanchas.Domain.Entities
{
    public class Field
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ComplexId { get; set; }
        public FieldType FieldType { get; set; }
        public FloorType FloorType { get; set; }
        public decimal HourPrice {  get; set; }
        public bool Ilumination { get; set; }
        public bool Covered { get; set; }
        public bool Active { get; set; }

        // Propiedad de navegacion
        public Complex Complex { get; set; } = null!;

        //Referencia 1 a n con TimeSlotField
        public List<TimeSlotField> TimeSlotsField { get; set; } = new List<TimeSlotField>(); 

        // Referencia 1 a n con los bloqueos recurrentes
        public ICollection<RecurringFieldBlock> RecurringCourtBlocks { get; set; } = null!;

        // Referencia 1 a N con las reservas
        public ICollection<Reservation> Reservations { get; set; } = null!;

    }
}
