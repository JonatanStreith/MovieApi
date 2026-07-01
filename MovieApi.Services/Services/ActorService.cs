using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Services.Services
{
    internal class ActorService : IActorService
    {
        private readonly IUnitOfWork _unit;
        private readonly IActorRepository _actors;

        public ActorService(IUnitOfWork context)
        {

            _unit = context ?? throw new ArgumentNullException(nameof(context));
            _actors = _unit.Actors;
        }

        public bool ActorExists(int id)
        {
            return _actors.ActorExists(id);
        }

        public Task<Actor> AddActorAsync(ActorDto actorDto)
        {
            return _actors.AddActorAsync(actorDto);
        }

        public Task<bool> AddActorToMovieAsync(int movieId, int actorId)
        {
            return _actors.AddActorToMovieAsync(movieId, actorId);
        }

        public Task<bool> DeleteActorAsync(int actorId)
        {
            return _actors.DeleteActorAsync(actorId);
        }

        public Task<ActorDto> GetActorAsync(int id)
        {
            return _actors.GetActorAsync(id);
        }

        public Task<IEnumerable<ActorDto>> GetActorsAsync()
        {
            return _actors.GetActorsAsync();
        }

        public Task<bool> SaveChangesAsync()
        {
            return _unit.CompleteAsync();
        }

        public Task<bool> UpdateActorAsync(int id, ActorDto actor)
        {
            return _actors.UpdateActorAsync(id, actor);
        }
    }
}
