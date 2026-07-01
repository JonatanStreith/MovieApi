using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _actorService.GetActorsAsync();


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


        //POST /api/actors
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorDto actorDto)
        {
            if (actorDto == null) return BadRequest("Incomplete or bad data.");

            var actor = await _actorService.AddActorAsync(actorDto);

            return CreatedAtAction("GetActor", new { id = actor.ActorId }, actor);
        }


        //PUT /api/actors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutActor(int id, ActorDto actor)
        {
            bool result = await _actorService.UpdateActorAsync(id, actor);

            if (!result) return NotFound($"The actor with the id {id} couldn't be found.");

            return NoContent();
        }

        //POST /api/movies/{movieId}/actors/{actorId} (lägg till aktör till film med roll)
        [HttpPost("/api/movies/{movieId}/actors/{actorId}")]
        public async Task<ActionResult<bool>> AddActorToMovie(int movieId, int actorId)
        {
            bool result = await _actorService.AddActorToMovieAsync(movieId, actorId);

            if (!result) return NotFound($"Movie {movieId} and/or actor {actorId} not found.");

            return NoContent();

        }

        //DELETE /api/actors/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteActor(int id)
        {
            bool result = await _actorService.DeleteActorAsync(id);

            if (!result) return NotFound($"The actor with the id {id} couldn't be found.");

            return NoContent();
        }
    }
}
