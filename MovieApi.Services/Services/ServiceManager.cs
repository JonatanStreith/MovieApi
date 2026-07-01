using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Services.Services
{
    public class ServiceManager : IServiceManager
    {

        public ServiceManager(IUnitOfWork context)
        {
            var _context = context ?? throw new ArgumentNullException(nameof(context));
            Movies = new MovieService(context);
            Reviews = new ReviewService(context);
            Actors = new ActorService(context);
        }

        public IMovieService Movies { get; }

        public IReviewService Reviews { get; }

        public IActorService Actors { get; }

    }
}
