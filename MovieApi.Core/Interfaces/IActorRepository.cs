using MovieApi.Core;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDto>> GetActorsAsync();
        Task<IEnumerable<ActorDto>> GetActorsAsync(PagingDto paging);

        Task<ActorDto> GetActorAsync(int id);

        Task<Actor> AddActorAsync(ActorDto actorDto);

        Task<Flag> AddActorToMovieAsync(int movieId, int actorId);

        Task<bool> UpdateActorAsync(int id, ActorDto actor);

        Task<bool> DeleteActorAsync(int actorId);

        bool ActorExists(int id);

        Task<bool> SaveChangesAsync();
    }
}
