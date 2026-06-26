using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {


            builder.HasOne(ma => ma.Movie)
                    .WithMany(m => m.MovieActor)
                    .HasForeignKey(ma => ma.MovieId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            

            builder.HasOne(ma => ma.Actor)
                    .WithMany(m => m.MovieActor)
                    .HasForeignKey(ma => ma.ActorId)
                    .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasKey(ma => ma.MovieActorId);

            builder.HasData(

                new MovieActor()
                {
                    MovieActorId = 1,
                    MovieId = 1,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    MovieActorId = 2,
                    MovieId = 1,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    MovieActorId = 3,
                    MovieId = 1,
                    ActorId = 3,
                },

                new MovieActor()
                {
                    MovieActorId = 4,
                    MovieId = 2,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    MovieActorId = 5,
                    MovieId = 2,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    MovieActorId = 6,
                    MovieId = 2,
                    ActorId = 4,
                },

                new MovieActor()
                {
                    MovieActorId = 7,
                    MovieId = 3,
                    ActorId = 1,
                },
                new MovieActor()
                {
                    MovieActorId = 8,
                    MovieId = 3,
                    ActorId = 2,
                },
                new MovieActor()
                {
                    MovieActorId = 9,
                    MovieId = 3,
                    ActorId = 5,
                }

                );
        }
    }
}