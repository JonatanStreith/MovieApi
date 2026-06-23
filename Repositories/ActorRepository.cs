using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public class ActorRepository : IActorRepository
    {

        private readonly MovieApiContext _context;

        public ActorRepository(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public bool ActorExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Actor> AddActorAsync(ActorDto actorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddActorToMovieAsync(int movieId, int actorId)
        {
            throw new NotImplementedException();
        }

        public Task<ActorDto> GetActorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ActorDto>> GetActorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateActorAsync(int id, ActorDto actor)
        {
            throw new NotImplementedException();
        }
    }
}
