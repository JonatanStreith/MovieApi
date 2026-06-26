using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasOne<MovieDetails>(m => m.MovieDetails)
                    .WithOne(d => d.Movie)
                    .HasForeignKey<MovieDetails>(d => d.MovieId);

            builder.HasKey(m => m.MovieId);
            builder.Property(m => m.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(m => m.Year)
                    .HasColumnType("int")
                    ;
            builder.Property(m => m.Genre)
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(m => m.Duration)
                    .HasColumnType("int")
                    ;

            builder.HasData(
                new Movie
                {
                    MovieId = 1,
                    Title = "Iron Man 1",
                    Year = 2008,
                    Genre = "Action",
                    Duration = 126
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "Iron Man 2",
                    Year = 2010,
                    Genre = "Action",
                    Duration = 124
                },
                new Movie
                {
                    MovieId = 3,
                    Title = "Iron Man 3",
                    Year = 2013,
                    Genre = "Action",
                    Duration = 130
                }
                );

        }
    }
}