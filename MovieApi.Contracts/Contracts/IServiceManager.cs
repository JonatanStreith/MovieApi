using MovieApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Contracts.Contracts
{
    internal interface IServiceManager
    {
        IMovieService Movies { get; }
        IReviewService Reviews { get; }
        IActorService Actors { get; }

    }
}
