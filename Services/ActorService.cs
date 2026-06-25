using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieApi.Contexts;

namespace MovieApi.Services
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

        public async Task<bool> AddActorToMovieAsync(int movieId, int actorId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var actor = await _context.Actors.FindAsync(actorId);

            if (movie == null || actor == null) return false;

            var movAct = new MovieActor()
            {
                MovieId = movieId,
                ActorId = actorId,
                Movie = movie,
                Actor = actor
            };

            movie.MovieActor.Add(movAct);
            actor.MovieActor.Add(movAct);

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateActorAsync(int id, ActorDto dto)
        {
            if (!ActorExists(id)) { return false; }

            var actor = await _context.Actors.FindAsync(id);

            actor.Name = dto.Name;
            actor.BirthYear = dto.BirthYear;

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteActorAsync(int actorId)
        {
            if (!ActorExists(actorId)) { return false; }

            var actor = await _context.Actors.FindAsync(actorId);

            _context.Actors.Remove(actor);

            await _context.SaveChangesAsync();
            return true;

        }

        public static ActorDto ConvertActorToDto(Actor actor)
        {
            return new ActorDto()
            {
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

        public static Actor ConvertDtoToActor(ActorDto dto)
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
