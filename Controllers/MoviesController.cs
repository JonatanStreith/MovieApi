using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Repositories;

[Route("api/movies")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService ??
                throw new ArgumentNullException(nameof(movieService)); ;
    }

    // GET: api/movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies(string? genre, int? year)
    {
        var movies = await _movieService.GetMoviesAsync(genre, year);


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

        return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
    }

    // PUT: api/movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movie)
    {

        bool result = await _movieService.UpdateMovieAsync(id, movie);

        if (!result) return NotFound();

        return NoContent();
    }

    // DELETE: api/movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var result = await _movieService.DeleteMovieAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
