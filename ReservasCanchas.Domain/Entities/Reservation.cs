using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.Domain.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        //Todavia no agregado
        //public int UserId { get; set; }
        public int FieldId { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly InitTime { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        // public PayType PayType { get; set; }
        public int TotalPrice { get; set; }
        public int PrecioPagado { get; set; }
        public ReservationType ReservationType { get; set; }
        public string BlockReason { get; set; } = string.Empty;


        // Propiedad de navegacion
        public Field Field { get; set; } = new Field();

    }
}
