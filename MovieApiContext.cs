using Microsoft.EntityFrameworkCore;

public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
{
    public DbSet<MovieApi.Models.Movie> Movie { get; set; } = default!;
    public DbSet<MovieApi.Models.MovieDetails> MovieDetails { get; set; } = default!;
    public DbSet<MovieApi.Models.Review> Review { get; set; } = default!;
    public DbSet<MovieApi.Models.MovieActor> MovieActor { get; set; } = default!;

}
