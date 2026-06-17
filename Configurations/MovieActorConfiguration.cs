using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {


            builder.HasOne<Movie>(/*ma => ma.Movie*/)
                    .WithMany(m => m.MovieActor)
                    .HasForeignKey(ma => ma.MovieId);

            builder.HasOne<Actor>(/*ma => ma.Actor*/)
                    .WithMany(m => m.MovieActor)
                    .HasForeignKey(ma => ma.ActorId);

            builder.HasKey(ma => ma.Id);

            builder.HasData(

                new MovieActor()
                {
                    Id = 1,
                    MovieId = 1,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    Id = 2,
                    MovieId = 1,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    Id = 3,
                    MovieId = 1,
                    ActorId = 3,
                },

                new MovieActor()
                {
                    Id = 4,
                    MovieId = 2,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    Id = 5,
                    MovieId = 2,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    Id = 6,
                    MovieId = 2,
                    ActorId = 4,
                },

                new MovieActor()
                {
                    Id = 7,
                    MovieId = 3,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    Id = 8,
                    MovieId = 3,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    Id = 9,
                    MovieId = 3,
                    ActorId = 5,
                }

                );
        }
    }
}