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
        public static Review ToReview(CreateReviewRequestDTO createReviewDTO, int userId)
        {
            return new Review
            {
                ReservationId = createReviewDTO.ReservationId,
                UserId = userId,
                Comment = createReviewDTO.Comment,
                Score = createReviewDTO.Score

            };
        }

        public static ReviewResponseDTO ToReviewResponseDTO(Review r)
        {
            return new ReviewResponseDTO
            {
                Id = r.Id,
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                Name = r.User.Name,
                LastName = r.User.LastName,
                Comment = r.Comment,
                Score = r.Score,
                CreationDate = r.CreationDate
            };
        }


           
        
    }
}
