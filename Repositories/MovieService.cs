using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MovieApi.Repositories
{
    public class MovieService : IMovieService
    {

        private readonly MovieApiContext _context;

        public MovieService(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(string? genre, int? year)
        {
            var movies =  _context.Movies.AsQueryable();

            if (genre != null)
            {
                movies = movies.Where(movie => movie.Genre == genre);
            }

            if(year != null)
            {
                movies = movies.Where(movie => movie.Year == year);
            }

            return await movies.OrderBy(m => m.Title).Select(movie => ConvertMovieToDto(movie)).ToListAsync();
        }

        public async Task<MovieDto?> GetMovieAsync(int id, bool fullData)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null) return null;

            var dto = ConvertMovieToDto(movie);

            if (fullData)
            {
                (var details, var reviews, var actors) = await GetAdditionalDataAsync(id);

                dto.Details = AddDetails(details, reviews, actors);
            }

            return dto;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(int id)
        {
            return await _context.Details.FindAsync(id);
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

            _context.Entry(movie).State = EntityState.Modified;

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

        public async Task<(MovieDetails?, List<Review>, List<Actor>)> GetAdditionalDataAsync(int id)
        {
            MovieDetails? details = await _context.Details.FirstOrDefaultAsync(detail => detail.MovieId == id);
            List<Review> reviews = await _context.Reviews.Where(review => review.MovieId == id).ToListAsync();
            var actorIds = await _context.MovieActors.Where(ma => ma.MovieId == id).Select(ma => ma.ActorId).ToListAsync();
            List<Actor> actors = await _context.Actors.Where(actor => actorIds.Contains(actor.ActorId)).ToListAsync();

            return (details, reviews, actors);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public MovieDetailDto AddDetails(MovieDetails details, List<Review> reviews, List<Actor> actors)
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
        public static MovieDto ConvertMovieToDto(Movie movie)
        {
            return new MovieDto()
            {
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration
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
