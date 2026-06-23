using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public interface IMovieRepository
    {

        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<MovieDto?> GetMovieAsync(int id, bool fullData);
        Task<MovieDetails> GetMovieDetailsAsync(int id);
        Task<Movie> AddMovieAsync(MovieCreateDto movie);
        Task UpdateMovieAsync(MovieUpdateDto movie);
        Task DeleteMovieAsync(int id);
    }
}
