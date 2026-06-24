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
        //POST /api/movies/{movieId}/ reviews
        //DELETE /api/reviews/{id}
    }
}   
