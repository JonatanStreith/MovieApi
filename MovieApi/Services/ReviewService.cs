using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Contexts;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class ReviewService : IReviewService
    {

        private readonly IAppDbContext _context;

        public ReviewService(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId)
        {

            var reviews = await _context.Reviews.Where(review => review.MovieId == movieId).OrderBy(r => r.ReviewerName).ToListAsync();
            return reviews.Select(review => ConvertReviewToDto(review));

        }

        public async Task<Review> AddReviewAsync(int movieId, ReviewDto reviewDto)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            var review = ConvertDtoToReview(reviewDto, movie.Title);

            movie.Reviews.Add(review);

            //_context.Entry(movie).State = EntityState.Modified;

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


        public static ReviewDto ConvertReviewToDto(Review review)
        {
            return new ReviewDto()
            {
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating,
                MovieId = review.MovieId
            };
        }

        public static Review ConvertDtoToReview(ReviewDto dto, string title)
        {
            return new Review()
            {
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                MovieId = dto.MovieId,
                MovieTitle = title
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
        public bool MovieExists(int? id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }


    }
}
