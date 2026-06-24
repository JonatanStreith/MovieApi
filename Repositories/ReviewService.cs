using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public class ReviewService : IReviewService
    {
        public async Task<Review> AddReviewAsync(ReviewDto reviewDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsAsync(int movieId)
        {
            throw new NotImplementedException();
        }
    }
}
