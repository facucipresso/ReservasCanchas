using ReservasCanchas.BusinessLogic.Dtos.Reservation;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic.Mappers
{
    public class ReviewMapper
    {
        public static CreateReviewResponseDTO ToCreateReviewResponseDTO(Review r)
        {
            return new CreateReviewResponseDTO
            {
                ReviewId = r.Id,
                ReservationId = r.ReservationId,
                UserId = r.IdUsuario,
                Comment = r.Comment,
                CreatedAt = DateTime.UtcNow
            };
        }


           
        
    }
}
