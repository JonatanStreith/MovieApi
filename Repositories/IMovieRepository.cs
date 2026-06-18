using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public interface IMovieRepository
    {

        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieAsync(int id);
        Task<MovieDetails> GetMovieDetailsAsync(int id);
        Task AddMovieAsync(MovieCreateDto movie);
        Task UpdateMovieAsync(MovieUpdateDto movie);
        Task DeleteMovieAsync(int id);
    }
}
