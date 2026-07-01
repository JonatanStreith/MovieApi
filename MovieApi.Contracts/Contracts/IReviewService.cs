using MovieApi.Dtos;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Contracts.Contracts
{
    public interface IReviewService
    {
        Task<Review> AddReviewAsync(int movieId, ReviewDto reviewDto);
        Task<bool> DeleteReviewAsync(int id);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId);

        public bool MovieExists(int? id);

        Task<bool> SaveChangesAsync();

    }
}
