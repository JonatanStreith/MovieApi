using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
{
    public DbSet<MovieApi.Models.Movie> Movies { get; set; } = default!;
    public DbSet<MovieApi.Models.MovieDetails> Details { get; set; } = default!;
    public DbSet<MovieApi.Models.Review> Reviews { get; set; } = default!;
    public DbSet<MovieApi.Models.MovieActor> MovieActors { get; set; } = default!;
    public DbSet<MovieApi.Models.Actor> Actors { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}