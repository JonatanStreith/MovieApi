using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public interface IMovieRepository
    {

        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieAsync();
        Task<MovieDetails> GetMovieDetailsAsync();
        Task AddMovieAsync(MovieCreateDto movie);
        Task UpdateMovieAsync(MovieUpdateDto movie);
        Task DeleteMovieAsync(int id);
    }
}
