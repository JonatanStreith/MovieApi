using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<ActorDto>> GetActorsAsync();


        Task<ActorDto> GetActorAsync(int id);

        Task<Actor> AddActorAsync(ActorDto actorDto);

        Task<bool> AddActorToMovieAsync(int movieId, int actorId);

        Task<bool> UpdateActorAsync(int id, ActorDto actor);

        Task<bool> DeleteActorAsync(int actorId);

        bool ActorExists(int id);

        Task<bool> SaveChangesAsync();
    }
}
