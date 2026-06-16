using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
            builder.Property(a => a.BirthYear)
                    .HasColumnType("int");

        }
    }
}
