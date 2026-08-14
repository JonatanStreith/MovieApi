using MovieApi.Dtos;
using MovieApi.Models;
using Microsoft.EntityFrameworkCore;
using MovieApi.Contexts;
using MovieApi.Interfaces;
using MovieApi.Core;

namespace MovieApi.Services
{
    public class ActorRepository : IActorRepository
    {

        private readonly IAppDbContext _context;

        public ActorRepository(IAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<IEnumerable<ActorDto>> GetActorsAsync()
        {
            var actors = await _context.Actors.OrderBy(a => a.Name).ToListAsync();


            return actors.Select(actor => ConvertActorToDto(actor));
        }

        public async Task<IEnumerable<ActorDto>> GetActorsAsync(PagingDto paging)
        {
            return await _context.Actors.OrderBy(a => a.Name)
                                .Skip(paging.PageSize * (paging.Page - 1)).Take(paging.PageSize)
                                .Select(actor => ConvertActorToDto(actor))
                                .ToListAsync();
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

        public async Task<Flag> AddActorToMovieAsync(int movieId, int actorId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            var actor = await _context.Actors.FindAsync(actorId);

            if (movie == null) return Flag.Movie_Not_Found;
            if (actor == null) return Flag.Actor_Not_Found;

            if (_context.MovieActors.Any(ma => ma.MovieId == movieId && ma.ActorId == actorId)) return Flag.MovieActor_Exists;

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

            return Flag.OK;
        }

        public async Task<bool> UpdateActorAsync(int id, ActorDto dto)
        {
            if (!ActorExists(id)) { return false; }

            var actor = await _context.Actors.FindAsync(id);

            actor.Name = dto.Name;
            actor.BirthYear = dto.BirthYear;

            //_context.Entry(actor).State = EntityState.Modified;

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
                Id = actor.ActorId,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }

        public static Actor ConvertDtoToActor(ActorDto dto)
        {
            return new Actor()
            {
                ActorId = dto.Id,
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
