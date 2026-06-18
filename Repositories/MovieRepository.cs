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

        public async Task<Movie?> GetMovieAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
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

    }
}
