using MovieApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Core.Interfaces
{
    public interface IUnitOfWork
    {

        IMovieRepository Movies { get; }
        IReviewRepository Reviews { get; }
        IActorRepository Actors { get; }
        Task<bool> CompleteAsync();
    }
}
