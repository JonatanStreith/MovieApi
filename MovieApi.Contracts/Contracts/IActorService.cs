using MovieApi.Core;
using MovieApi.Dtos;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace MovieApi.Contracts.Contracts
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetActorsAsync();

        Task<IEnumerable<ActorDto>> GetActorsAsync(PagingDto paging);

        Task<IEnumerable<ActorDto>> GetActorsAsync(int movieId);

        Task<ActorDto> GetActorAsync(int id);

        Task<Actor> AddActorAsync(ActorDto actorDto);

        Task<Flag> AddActorToMovieAsync(int movieId, int actorId);

        Task<bool> UpdateActorAsync(int id, ActorDto actor);

        Task<bool> DeleteActorAsync(int actorId);

        bool ActorExists(int id);

        bool MovieExists(int movieId);

        Task<bool> SaveChangesAsync();

    }
}
