using Microsoft.EntityFrameworkCore.Metadata;
using MovieApi.Contexts;
using MovieApi.Core.Interfaces;
using MovieApi.Interfaces;
using MovieApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Data.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IAppDbContext _context;
        public UnitOfWork(MovieApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Movies = new MovieService(context);
            Reviews = new ReviewService(context);
            Actors = new ActorService(context);
        }

        public IMovieService Movies { get; }

        public IReviewService Reviews { get; }

        public IActorService Actors { get; }

        public async Task<bool> CompleteAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}

