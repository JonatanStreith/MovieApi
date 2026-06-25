using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MovieApi.Contexts
{

    public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options), IAppDbContext
    {
        public DbSet<MovieApi.Models.Movie> Movies { get; set; } = default!;
        public DbSet<MovieApi.Models.MovieDetails> Details { get; set; } = default!;
        public DbSet<MovieApi.Models.Review> Reviews { get; set; } = default!;
        public DbSet<MovieApi.Models.MovieActor> MovieActors { get; set; } = default!;
        public DbSet<MovieApi.Models.Actor> Actors { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
    }
}