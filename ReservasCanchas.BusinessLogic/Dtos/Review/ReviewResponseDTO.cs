using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Review
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ComplexName { get; set; }
        public int ReservationId { get; set; }
        public string? Comment { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
