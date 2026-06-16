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
        }
    }
}