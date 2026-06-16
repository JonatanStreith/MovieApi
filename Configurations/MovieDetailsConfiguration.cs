using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class MovieDetailsConfiguration : IEntityTypeConfiguration<MovieDetails>
    {
        public void Configure(EntityTypeBuilder<MovieDetails> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Synopsis)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(m => m.Language)
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(m => m.Budget)
                    .HasColumnType("int")
                    ;
            builder.Property(m => m.MovieId)
                    .HasColumnType("int")
                    ;
            builder.Property(m => m.MovieTitle)
        .HasMaxLength(50)
        .HasColumnType("nvarchar")
        ;

        }
    }
}
