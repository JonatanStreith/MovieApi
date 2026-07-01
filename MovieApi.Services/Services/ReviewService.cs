using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Services.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unit;
        private readonly IReviewRepository _reviews;

        public ReviewService(IUnitOfWork context)
        {

            _unit = context ?? throw new ArgumentNullException(nameof(context));
            _reviews = _unit.Reviews;
        }

        public Task<Review> AddReviewAsync(int movieId, ReviewDto reviewDto)
        {
            return _reviews.AddReviewAsync(movieId, reviewDto);
        }

        public Task<bool> DeleteReviewAsync(int id)
        {
            return _reviews.DeleteReviewAsync(id);
        }

        public Task<IEnumerable<ReviewDto>> GetReviewsAsync(int movieId)
        {
            return _reviews.GetReviewsAsync(movieId);
        }

        public bool MovieExists(int? id)
        {
            return _reviews.MovieExists(id);
        }

        public Task<bool> SaveChangesAsync()
        {
            return _unit.CompleteAsync();
        }
    }
}
