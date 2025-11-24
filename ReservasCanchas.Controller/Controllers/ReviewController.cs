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

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReviewById([FromRoute] int id)
        {
            var review = await _reviewBusinessLogic.GetReviewByIdAsync(id);
            return Ok(review);
        }

        [HttpGet("my")]
        public async Task<ActionResult<ReviewResponseDTO>> GetMyReviews()
        {
            var reviews = await _reviewBusinessLogic.GetReviewsByUserAsync();
            return Ok(reviews);
        }

        [HttpGet("by-reservation/{reservationId}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReviewByReservationId([FromRoute] int reservationId)
        {
            var review = await _reviewBusinessLogic.GetReviewByReservationIdAsync(reservationId);
            return Ok(review);
        }

        [HttpGet]
        public async Task<ActionResult<ReviewResponseDTO>> GetReviewsByComplexId([FromQuery] int complexId)
        {
            var reviews = await _reviewBusinessLogic.GetReviewsByComplexIdAsync(complexId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewResponseDTO>> CreateReview([FromBody] CreateReviewRequestDTO createReviewDTO)
        {
            var reviewCreated = await _reviewBusinessLogic.CreateReviewAsync(createReviewDTO);
            return CreatedAtAction(nameof(GetReviewById), new { id = reviewCreated.Id }, reviewCreated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview([FromRoute] int id)
        {
            await _reviewBusinessLogic.DeleteReview(id);
            return NoContent();
        }
    }
}
