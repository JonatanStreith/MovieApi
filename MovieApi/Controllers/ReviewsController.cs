using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewRepository _reviewService;
        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _reviewService = unitOfWork.Reviews ??
                    throw new ArgumentNullException(nameof(unitOfWork.Reviews)); ;
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
        public async Task<ActionResult<bool>> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result) return NotFound($"No movie with id {id} exists in the database.");
            return NoContent();
        }
    }
}   
