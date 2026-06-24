using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public class ReviewService : IReviewService
    {

        private readonly MovieApiContext _context;

        public ReviewService(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsAsync(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie == null) return null;

            var reviews = movie.Reviews;

            return reviews.Select(review => ConvertReviewToDto(review)).OrderBy(review => review.ReviewerName).ToList();
        }

        public async Task<Review> AddReviewAsync(int movieId, ReviewDto reviewDto)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie == null) return null;

            var review = ConvertDtoToReview(reviewDto);

            movie.Reviews.Add(review);

            await SaveChangesAsync();

            return review;

        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            if (!ReviewExists(id)) { return false; }

            var review = await _context.Reviews.FindAsync(id);

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();
            return true;
        }


        public ReviewDto ConvertReviewToDto(Review review)
        {
            return new ReviewDto()
            {
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating,
                MovieId = review.MovieId
            };
        }

        public Review ConvertDtoToReview(ReviewDto dto)
        {
            return new Review()
            {
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                MovieId = dto.MovieId
            };
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }


    }
}
