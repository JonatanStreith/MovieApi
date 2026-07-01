
using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Services.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMovieRepository _movies;

        public MovieService(IUnitOfWork context)
        {

            _unit = context ?? throw new ArgumentNullException(nameof(context));
            _movies = _unit.Movies;
        }

        public Task<Movie> AddMovieAsync(MovieCreateDto dto)
        {
            return _movies.AddMovieAsync(dto);
        }

        public Task<bool> DeleteMovieAsync(int id)
        {
            return _movies.DeleteMovieAsync(id);
        }

        public Task<MovieDto?> GetMovieAsync(int id, bool fullData)
        {
            return _movies.GetMovieAsync(id, fullData);
        }

        public Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            return _movies.GetMovieDetailsAsync(movieId);
        }

        public Task<IEnumerable<MovieDto>> GetMoviesAsync(string? genre, int? year)
        {
            return _movies.GetMoviesAsync(genre, year);
        }

        public bool MovieExists(int? id)
        {
            return _movies.MovieExists(id);
        }

        public Task<bool> SaveChangesAsync()
        {
            return _unit.CompleteAsync();
        }

        public Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            return _movies.UpdateMovieAsync(id, dto);
        }
    }
}
