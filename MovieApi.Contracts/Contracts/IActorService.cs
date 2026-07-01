using MovieApi.Dtos;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Contracts.Contracts
{
    internal interface IActorService
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
