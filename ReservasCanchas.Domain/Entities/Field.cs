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
        public int ComplexId { get; set; }
        public FieldType FieldType { get; set; }
        public FloorType FloorType { get; set; }
        public int HourPrice {  get; set; }
        public bool Ilumination { get; set; }
        public bool Covered { get; set; }
        public bool Active { get; set; }

        // Propiedad de navegacion
        public Complejo Complex { get; set; } = new Complejo();

        //Referencia 1 a 1 con TimeSlotComplex, esto me permitiria hacer t
        public TimeSlotField TimeSlotField { get; set; } = new TimeSlotField(); 

        // Referencia 1 a n con los bloqueos recurrentes
        public ICollection<RecurringCourtBlock> recurringCourtBlocks { get; set; } = null!;

        // Referencia 1 a N con las reservas
        public ICollection<Reservation> Reservations { get; set; } = null!;
    }
}
