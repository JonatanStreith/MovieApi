using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieApiContext _context;

        public MovieRepository(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }




        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {

            return await _context.Movies.OrderBy(m => m.Id).ToListAsync();

        }

        public async Task<MovieDto?> GetMovieAsync(int id, bool fullData)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null) return null;

            if (fullData) 
            {
                (var details, var reviews, var actors) = await GetAdditionalDataAsync(id);

                //var result = reviews.Select(review => ConvertReviewToDto(review)).ToList();

                return new MovieDto()
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Duration = movie.Duration,

                    Details = new MovieDetailDto()
                    {
                        MovieId = id,
                        Synopsis = details.Synopsis,
                        Language = details.Language,
                        Budget = details.Budget,

                        Reviews = reviews.Select(review => ConvertReviewToDto(review)).ToList(),
                        Actors = actors.Select(actor => ConvertActorToDto(actor)).ToList()
                    }
                };
            }

            else
            {
                return new MovieDto()
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Duration = movie.Duration
                };
            }
        }
            //var movie = await _context.Movies.FindAsync(id);

            //if (movie == null) return null;

            //MovieDto movieWithDetails = new MovieDto()
            //{
            //    MovieId = movie.Id,

            //};
        public async Task<MovieDetails> GetMovieDetailsAsync(int id)
        {
            return await _context.Details.FindAsync(id);
        }

        public async Task AddMovieAsync(MovieCreateDto movie)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMovieAsync(MovieUpdateDto movie)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<(MovieDetails, List<Review>, List<Actor>)> GetAdditionalDataAsync(int id)
        {
            var details = await _context.Details.FirstOrDefaultAsync(detail => detail.MovieId == id);
            var reviews = await _context.Reviews.Where(review => review.MovieId == id).ToListAsync();
            var actorIds = await _context.MovieActors.Where(ma => ma.MovieId == id).Select(ma => ma.ActorId).ToListAsync();
            var actors = await _context.Actors.Where(actor => actorIds.Contains(actor.Id)).ToListAsync();

            return (details, reviews, actors);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
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
        public ActorDto ConvertActorToDto(Actor actor)
        {
            return new ActorDto()
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

    }
}
