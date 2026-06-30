//using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IMovieService
    {

        Task<IEnumerable<MovieDto>> GetMoviesAsync(string? genre, int? year);
        Task<MovieDto?> GetMovieAsync(int id, bool fullData);
        Task<MovieDetails> GetMovieDetailsAsync(int movieId);
        Task<Movie> AddMovieAsync(MovieCreateDto dto);
        Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto);
        Task<bool> DeleteMovieAsync(int id);

        
        bool MovieExists(int? id);

        Task<bool> SaveChangesAsync();
    }
}
