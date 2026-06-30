using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {


            builder.HasKey(a => a.ActorId);
            builder.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
            builder.Property(a => a.BirthYear)
                    .HasColumnType("int");

            builder.HasData(
                new Actor()
                {
                    ActorId = 1,
                    Name = "Robert Downey Jr.",
                    BirthYear = 1965
                },
                new Actor()
                {
                    ActorId = 2,
                    Name = "Gwyneth Paltrow",
                    BirthYear = 1972
                },
                new Actor()
                {
                    ActorId = 3,
                    Name = "Terrence Howard",
                    BirthYear = 1969
                },
                new Actor()
                {
                    ActorId = 4,
                    Name = "Mickey Rourke",
                    BirthYear = 1952
                },
                new Actor()
                {
                    ActorId = 5,
                    Name = "Guy Pearce",
                    BirthYear = 1967
                }
                );

        }
    }
}
