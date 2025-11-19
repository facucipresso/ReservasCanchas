using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Review;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewBusinessLogic _reviewBusinessLogic;
        public ReviewController(ReviewBusinessLogic reviewBusinessLogic)
        {
            _reviewBusinessLogic = reviewBusinessLogic;
        }

        [HttpPost("reservations/{reservationId}")]
        public async Task<ActionResult<CreateReviewResponseDTO>> CreateReview([FromRoute] int reservationId, [FromBody] CreateReviewRequestDTO comment)
        {
            var reviewCreated = await _reviewBusinessLogic.CreateReviewAsync(reservationId, comment);
            return reviewCreated;
        }
    }
}
