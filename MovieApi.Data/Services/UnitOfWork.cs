using Microsoft.EntityFrameworkCore.Metadata;
using MovieApi.Core.Interfaces;
using MovieApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Data.Services
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IMovieService Movies => throw new NotImplementedException();

        public IReviewService Reviews => throw new NotImplementedException();

        public IActorService Actors => throw new NotImplementedException();

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
