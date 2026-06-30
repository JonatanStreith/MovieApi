using MovieApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Core.Interfaces
{
    public interface IUnitOfWork
    {

        IMovieService Movies { get; }
        IReviewService Reviews { get; }
        IActorService Actors { get; }
        Task CompleteAsync();
    }
}
