using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;

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

        //GET /api/movies/{movieId}/reviews
        [HttpGet("/api/movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
        {
            if (!_reviewService.MovieExists(movieId)) return NotFound($"No movie with id {movieId} exists in the database.");

            var reviews = await _reviewService.GetReviewsAsync(movieId);


            return Ok(reviews);
        }
        //POST /api/movies/{movieId}/reviews
        [HttpPost("/api/movies/{movieId}/reviews")]
        public async Task<ActionResult<Review>> PostReview(int movieId, ReviewDto reviewDto)
        {
            if (reviewDto == null || reviewDto.MovieId != movieId) return BadRequest("Incomplete or bad data.");

            if(!_reviewService.MovieExists(movieId)) return BadRequest($"No movie with id {movieId} exists in the database.");

            Review review = await _reviewService.AddReviewAsync(movieId, reviewDto);

            return CreatedAtAction("PostReview", new { id = review.Id }, review);
        }


        //DELETE /api/reviews/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}   
