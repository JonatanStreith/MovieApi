using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Repositories
{
    public class ActorService : IActorService
    {

        private readonly MovieApiContext _context;

        public ActorService(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<IEnumerable<ActorDto>> GetActorsAsync()
        {
            var actors = await _context.Actors.OrderBy(a => a.Name).ToListAsync();


            return actors.Select(actor => ConvertActorToDto(actor));
        }

        public async Task<ActorDto> GetActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null) return null;


            return ConvertActorToDto(actor);
            
        }

        public async Task<Actor> AddActorAsync(ActorDto actorDto)
        {
            var actor = ConvertDtoToActor(actorDto);
            if (actor == null) return null;

            _context.Actors.Add(actor);

            await SaveChangesAsync();

            return actor;
        }

        public Task<bool> AddActorToMovieAsync(int movieId, int actorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateActorAsync(int id, ActorDto actor)
        {
            throw new NotImplementedException();
        }

        public ActorDto ConvertActorToDto(Actor actor)
        {
            return new ActorDto()
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

        public Actor ConvertDtoToActor(ActorDto dto)
        {
            return new Actor()
            {
                Name = dto.Name,
                BirthYear = dto.BirthYear
            };
        }

        public bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
