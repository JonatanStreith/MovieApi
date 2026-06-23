using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDto>> GetActorsAsync();


        Task<ActorDto> GetActorAsync(int id);

        Task<Actor> AddActorAsync(ActorDto actorDto);

        Task<bool> UpdateActorAsync(int id, ActorDto actor);

        Task<bool> AddActorToMovieAsync(int movieId, int actorId);

        bool ActorExists(int id);
    }
}
