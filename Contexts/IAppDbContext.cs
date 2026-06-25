using Microsoft.EntityFrameworkCore;

namespace MovieApi.Contexts
{
    public interface IAppDbContext
    {

            DbSet<MovieApi.Models.Movie> Movies { get; set; }
            DbSet<MovieApi.Models.MovieDetails> Details { get; set; }
            DbSet<MovieApi.Models.Review> Reviews { get; set; }
            DbSet<MovieApi.Models.MovieActor> MovieActors { get; set; }
            DbSet<MovieApi.Models.Actor> Actors { get; set; }


            Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
