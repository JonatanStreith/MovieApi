using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Repositories;

namespace MovieApi.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        public ActorsController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository ??
                    throw new ArgumentNullException(nameof(actorRepository)); ;
        }

        //GET /api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _actorRepository.GetActorsAsync();


            return Ok(actors);
        }


        //GET /api/actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _actorRepository.GetActorAsync(id);

            if (actor == null) return NotFound($"The actor with the id {id} couldn't be found.");

            return Ok(actor);
        }


        //POST /api/actors
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorDto actorDto)
        {
            if (actorDto == null) return BadRequest("Incomplete or bad data.");


            var actor = await _actorRepository.AddActorAsync(actorDto);

            return CreatedAtAction("GetActor", new { id = actor.ActorId }, actor);


            return null;
        }


        //PUT /api/actors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDto actor)
        {
            bool result = await _actorRepository.UpdateActorAsync(id, actor);

            if (!result) return NotFound();

            return NoContent();
        }

        //POST /api/movies/{movieId}/actors/{actorId} (lägg till aktör till film med roll)
        [HttpPost("{movieId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
        {
            bool result = await _actorRepository.AddActorToMovieAsync(movieId, actorId);

            if (!result) return NotFound();

            return NoContent();

        }


    }
}
