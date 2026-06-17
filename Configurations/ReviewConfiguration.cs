using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne<Movie>().WithMany().HasForeignKey(r => r.MovieId);

            builder.HasKey(r => r.Id);
            builder.Property(r => r.ReviewerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(r => r.Rating)
                    .IsRequired()
                    .HasColumnType("int")
                    ;
            builder.Property(r => r.Comment)
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar")
                    ;
            builder.Property(r => r.MovieId)
                    .IsRequired()
                    .HasColumnType("int")
                    ;
            builder.Property(r => r.MovieTitle)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    ;

            builder.HasData(
                new Review()
                {
                    Id = 1,
                    ReviewerName = "Chester A. Bum",
                    Rating = 5,
                    Comment = "OMIGOD THIS IS THE BEST MOVIE EVER!",
                    MovieId = 1,
                    MovieTitle = "Iron Man 1"
                },
                new Review()
                {
                    Id = 2,
                    ReviewerName = "Snooty McNitpick",
                    Rating = 1,
                    Comment = "A distasteful look into the horrifying military complex. Also, Marvel just keeps pumping these out. This is, what, the fifth one in the series?",
                    MovieId = 1,
                    MovieTitle = "Iron Man 1"
                },
                new Review()
                {
                    Id = 3,
                    ReviewerName = "StarkLovr4908",
                    Rating = 5,
                    Comment = "Tony looks so hot in this one. Shame about the ending. I wrote a fanfic that fixed everything, read it at http://wwwfanfic.con/340694hgio/a/",
                    MovieId = 3,
                    MovieTitle = "Iron Man 3"
                },
                new Review()
                {
                    Id = 4,
                    ReviewerName = "Doug Spoilerton",
                    Rating = 5,
                    Comment = "A fascinating adaptation of long-running comics continuity adapted into film. A shame TONY DIES IN ENDGAME!",
                    MovieId = 1,
                    MovieTitle = "Iron Man 1"
                },
                new Review()
                {
                    Id = 5,
                    ReviewerName = "Richard Normalman",
                    Rating = 4,
                    Comment = "Are all these reviews made up? Whatever. I liked the movie.",
                    MovieId = 2,
                    MovieTitle = "Iron Man 2"
                }


                );
        }
    }
}
