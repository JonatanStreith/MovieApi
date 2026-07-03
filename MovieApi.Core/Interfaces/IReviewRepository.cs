//using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> AddReviewAsync(int movieId, ReviewDto reviewDto);
        Task<bool> DeleteReviewAsync(int id);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId, PagingDto paging);

        public bool MovieExists(int? id);

        Task<bool> SaveChangesAsync();
    }
}
