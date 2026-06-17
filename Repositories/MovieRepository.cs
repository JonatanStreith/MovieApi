using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieApiContext _context;

        public MovieRepository(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task AddMovieAsync(MovieCreateDto movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetMovieAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetails> GetMovieDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieAsync(MovieUpdateDto movie)
        {
            throw new NotImplementedException();
        }
    }
}
