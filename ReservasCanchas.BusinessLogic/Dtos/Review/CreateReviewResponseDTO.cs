using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Dtos.Review
{
    public class CreateReviewResponseDTO
    {
        public int ReviewId { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
