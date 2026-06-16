using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasOne<Movie>()
                    .WithMany()
                    .HasForeignKey(ma => ma.MovieId);

            builder.HasOne<Actor>()
                    .WithMany()
                    .HasForeignKey(ma => ma.ActorId);

            builder.HasKey(ma => ma.Id);
            builder.Property(ma => ma.Title)
                    .IsRequired()
                    .HasColumnType("nvarchar");
            builder.Property(ma => ma.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar");

            builder.HasData(

                new MovieActor()
                {
                    Id = 1,
                    MovieId = 1,
                    Title = "Iron Man 1",
                    ActorId = 1,
                    Name = "Robert Downey Jr"
                },
                new MovieActor()
                {
                    Id = 2,
                    MovieId = 1,
                    Title = "Iron Man 1",
                    ActorId = 2,
                    Name = "Gwyneth Paltrow"
                },
                new MovieActor()
                {
                    Id = 3,
                    MovieId = 1,
                    Title = "Iron Man 1",
                    ActorId = 3,
                    Name = "Terrence Howard"
                },

                new MovieActor()
                {
                    Id = 4,
                    MovieId = 2,
                    Title = "Iron Man 2",
                    ActorId = 1,
                    Name = "Robert Downey Jr"
                },
                new MovieActor()
                {
                    Id = 5,
                    MovieId = 2,
                    Title = "Iron Man 2",
                    ActorId = 2,
                    Name = "Gwyneth Paltrow"
                },
                new MovieActor()
                {
                    Id = 6,
                    MovieId = 2,
                    Title = "Iron Man 2",
                    ActorId = 4,
                    Name = "Mickey Rourke"
                },

                new MovieActor()
                {
                    Id = 7,
                    MovieId = 3,
                    Title = "Iron Man 3",
                    ActorId = 1,
                    Name = "Robert Downey Jr"
                },
                new MovieActor()
                {
                    Id = 8,
                    MovieId = 3,
                    Title = "Iron Man 3",
                    ActorId = 2,
                    Name = "Gwyneth Paltrow"
                },
                new MovieActor()
                {
                    Id = 9,
                    MovieId = 3,
                    Title = "Iron Man 3",
                    ActorId = 5,
                    Name = "Guy Pearce"
                }

                );
        }
    }
}