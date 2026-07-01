using MovieApi.Dtos;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Contracts.Contracts
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
