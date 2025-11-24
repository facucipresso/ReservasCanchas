using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.Domain.Entities;

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

        [HttpGet("/{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReviewById([FromRoute] int id)
        {
            ReviewResponseDTO review = await _reviewBusinessLogic.GetReviewByIdAsync(id);
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<CreateReviewResponseDTO>> CreateReview([FromBody] CreateReviewRequestDTO createReviewDTO)
        {
            var reviewCreated = await _reviewBusinessLogic.CreateReviewAsync(createReviewDTO);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = reviewCreated.ReviewId }, reviewCreated);
        }
    }
}
