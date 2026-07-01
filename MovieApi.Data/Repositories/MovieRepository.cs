using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using MovieApi.Contexts;
using MovieApi.Interfaces;

namespace MovieApi.Services
{
    public class MovieRepository : IMovieService
    {

        private readonly IAppDbContext _context;

        public MovieRepository(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(string? genre, int? year)
        {
            var movies = _context.Movies.AsQueryable();

            if (genre != null)
            {
                movies = movies.Where(movie => movie.Genre == genre);
            }

            if (year != null)
            {
                movies = movies.Where(movie => movie.Year == year);
            }

            return await movies.OrderBy(m => m.Title).Select(movie => ConvertMovieToDto(movie)).ToListAsync();
        }

        public async Task<MovieDto?> GetMovieAsync(int id, bool fullData)
        {
            Movie movie;

            if (fullData)
            {
                movie = await _context.Movies
                    .Include(movie => movie.MovieDetails)
                    .Include(movie => movie.Reviews)
                    .Include(movie => movie.MovieActor)
                .Where(movie => movie.MovieId == id).FirstAsync();
            }

            else
            {
                movie = await _context.Movies
                .Where(movie => movie.MovieId == id).FirstAsync();

            }

            if (movie == null) return null;



            var dto = new MovieDto()
            {
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
            };

            if (fullData)
            {
                dto.Details = new MovieDetailDto();
                if (movie.MovieDetails != null)
                {
                    dto.Details.MovieId = movie.MovieId;
                    dto.Details.Synopsis = movie.MovieDetails.Synopsis;
                    dto.Details.Language = movie.MovieDetails.Language;
                    dto.Details.Budget = movie.MovieDetails.Budget;
                }
                dto.Details.Reviews = movie.Reviews.Select(review => ConvertReviewToDto(review)).ToList();

                var actorIds = await _context.MovieActors.Where(ma => ma.MovieId == movie.MovieId).Select(ma => ma.ActorId).ToListAsync();
                List<Actor> actors = await _context.Actors.Where(actor => actorIds.Contains(actor.ActorId)).ToListAsync();

                dto.Details.Actors = actors.Select(actor => ConvertActorToDto(actor)).ToList();
            }

            return dto;
        }

        public static MovieDto ConvertMovieToDto(Movie movie)
        {
            var dto = new MovieDto()
            {
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
            };


            return dto;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var result = await _context.Movies
                .Where(movie => movie.MovieId == movieId)
                .Select(movie => movie.MovieDetails)
                .FirstAsync();

            return result;
        }

        public async Task<Movie> AddMovieAsync(MovieCreateDto dto)
        {
            var movie = new Movie()
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration
            };

            _context.Movies.Add(movie);

            await SaveChangesAsync();

            return movie;
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            if (!MovieExists(id)) { return false; }

            var movie = await _context.Movies.FindAsync(id);

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = dto.Genre;
            movie.Duration = dto.Duration;

            //_context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;

        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            if (!MovieExists(id)) { return false; }

            var movie = await _context.Movies.FindAsync(id);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public static MovieDetailDto AddDetails(MovieDetails details, List<Review> reviews, List<Actor> actors)
        {
            return new MovieDetailDto()
            {

                MovieId = details.MovieId,
                Synopsis = details.Synopsis,
                Language = details.Language,
                Budget = details.Budget,

                Reviews = reviews.Select(review => ConvertReviewToDto(review)).ToList(),
                Actors = actors.Select(actor => ConvertActorToDto(actor)).ToList()
            };
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
        public static ActorDto ConvertActorToDto(Actor actor)
        {
            return new ActorDto()
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

        public static Movie ConvertDtoToMovie(MovieDto dto)
        {
            var movie = new Movie()
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration
            };

            return movie;
        }

        public bool MovieExists(int? id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
