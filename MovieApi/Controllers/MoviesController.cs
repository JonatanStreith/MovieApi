using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;

[Route("api/v{version:apiVersion}/movies")]
[ApiController]

[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MoviesController(IServiceManager manager)
    {
        _movieService = manager.Movies ??
                throw new ArgumentNullException(nameof(manager.Movies)); ;
    }

    // GET: api/movies
    [HttpGet]
    [MapToApiVersion("1.0")]        //This ensures it's the one accessed when we use v1.0
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(string? genre, int? year)
    {

        var movies = await _movieService.GetMoviesAsync(genre, year);


        return Ok(movies);
    }

    // GET: api/movies
    [HttpGet]
    [MapToApiVersion("2.0")]        //This ensures it's the one accessed when we use v1.0
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(string? genre, int? year, int pageSize = 10, int page = 1)
    {
        if (pageSize < 10 || pageSize > 100 || page < 1) return BadRequest("Illegitimate paging parameters.");

        var movies = await _movieService.GetMoviesAsync(genre, year, pageSize, page);


        return Ok(movies);
    }

    // GET: api/movies/1?fulldata=true
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id, bool fullData = false)
    {
        var movie = await _movieService.GetMovieAsync(id, fullData);

        if (movie == null)
        {
            return NotFound($"The movie with the id {id} couldn't be found.");
        }

        return Ok(movie);
    }

    // GET /api/movies/{id}/details
    [HttpGet("{movieId}/details")]
    public async Task<ActionResult<Movie>> GetMovieDetails(int movieId)
    {
        if(!_movieService.MovieExists(movieId)) return NotFound($"The movie with the id {movieId} couldn't be found.");

        var movieDetails = await _movieService.GetMovieDetailsAsync(movieId);

        if (movieDetails == null)
        {
            return NotFound("Movie Details not found for movie.");
        }

        return Ok(movieDetails);
    }

    // POST: api/movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(MovieCreateDto movieCreateDto)
    {
        var movie = await _movieService.AddMovieAsync(movieCreateDto);

        if (movie == null) return BadRequest("Movie could not be added; faulty data.");

        return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
    }

    // PUT: api/movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> PutMovie(int movieId, MovieUpdateDto movie)
    {

        bool result = await _movieService.UpdateMovieAsync(movieId, movie);

        if (!result) return NotFound($"The movie with the id {movieId} couldn't be found.");

        return NoContent();
    }

    // DELETE: api/movies/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteMovie(int movieId)
    {
        var result = await _movieService.DeleteMovieAsync(movieId);
        if (!result) return NotFound($"The movie with the id {movieId} couldn't be found.");
        return NoContent();
    }
}
