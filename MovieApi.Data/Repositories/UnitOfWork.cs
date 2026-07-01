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
            Movies = new MovieRepository(context);
            Reviews = new ReviewRepository(context);
            Actors = new ActorRepository(context);
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

