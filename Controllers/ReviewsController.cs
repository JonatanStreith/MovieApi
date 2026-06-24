using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Repositories;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService ??
                    throw new ArgumentNullException(nameof(reviewService)); ;
        }

        //GET /api/reviews/{movieId}/reviews
        [HttpGet("{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
        {
            var reviews = await _reviewService.GetReviewsAsync(movieId);


            return Ok(reviews);
        }
        //POST /api/reviews/{movieId}/reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(int movieId, ReviewDto reviewDto)
        {
            if (reviewDto == null) return BadRequest("Incomplete or bad data.");
            

            Review review = await _reviewService.AddReviewAsync(reviewDto);

            return CreatedAtAction("GetReviews", new { id = review.Id }, review);
        }


        //DELETE /api/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}   
