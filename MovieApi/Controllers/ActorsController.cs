using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Contracts.Contracts;
using MovieApi.Core;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace MovieApi.Controllers
{
    [Route("api/v{version:apiVersion}/actors")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]

    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IServiceManager manager)
        {
            _actorService = manager.Actors ??
                    throw new ArgumentNullException(nameof(manager.Actors)); ;
        }

        //GET /api/actors
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _actorService.GetActorsAsync();


            return Ok(actors);
        }

        //GET /api/actors
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors([FromQuery] PagingDto paging)
        {
            if (paging.PageSize < 10 || paging.PageSize > 100 || paging.Page < 1) return BadRequest("Illegitimate paging parameters.");

            var actors = await _actorService.GetActorsAsync(paging);

            if (paging != null)
            {
                var meta = MetaDataDto.GetMeta(paging, actors.Count());
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(meta));
            }

            return Ok(actors);
        }


        //GET /api/actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _actorService.GetActorAsync(id);

            if (actor == null) return NotFound($"The actor with the id {id} couldn't be found.");

            return Ok(actor);
        }

        // GET /api/movies/{id}/actors
        [HttpGet("/api/movies/{movieId}/actors")]
        public async Task<ActionResult<Actor>> GetMovieActors(int movieId)
        {
            if (!_actorService.MovieExists(movieId)) return NotFound($"The movie with the id {movieId} couldn't be found.");

            var actors = await _actorService.GetActorsAsync(movieId);

            if (actors == null)
            {
                return NotFound("Actors not found for movie.");
            }

            return Ok(actors);
        }

        //POST /api/actors
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Actor>> PostActor(ActorDto actorDto)
        {
            if (actorDto == null) return BadRequest("Incomplete or bad data.");

            var actor = await _actorService.AddActorAsync(actorDto);

            return NoContent(); //CreatedAtAction("GetActor", new { id = actor.ActorId }, actor);
        }


        //PUT /api/actors/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> PutActor(int id, ActorDto actor)
        {
            bool result = await _actorService.UpdateActorAsync(id, actor);

            if (!result) return NotFound($"The actor with the id {id} couldn't be found.");

            return NoContent();
        }

        //POST /api/movies/{movieId}/actors/{actorId} (lägg till aktör till film med roll)
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        [Authorize]
        public async Task<ActionResult<bool>> AddActorToMovie(int movieId, int actorId)
        {
            Flag result = await _actorService.AddActorToMovieAsync(movieId, actorId);

            if (result == Flag.Movie_Not_Found) return NotFound($"Movie {movieId} not found.");
            if (result == Flag.Actor_Not_Found) return NotFound($"Actor {actorId} not found.");
            if (result == Flag.MovieActor_Exists) return NotFound($"That actor has already been added.");

            return NoContent();

        }

        //DELETE /api/actors/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteActor(int id)
        {
            bool result = await _actorService.DeleteActorAsync(id);

            if (!result) return NotFound($"The actor with the id {id} couldn't be found.");

            return NoContent();
        }
    }
}
