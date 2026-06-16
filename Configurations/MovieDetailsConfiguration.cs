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
                    .HasMaxLength(2500)
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

            builder.HasData(
                new MovieDetails()
                {
                    Id = 1,
                    Synopsis = "After being captured by terrorists following a missile demonstration in Afghanistan, multi-billionaire Tony Stark uses his brilliant intellect to devise a powered armor to escape. Being an irresponsible, wealthy playboy before, he (literally) has a change of heart regarding his company policies and dedicates himself to cleaning up Stark Industries' patented weapons and taking care of the terrorist group that got their hands on them. To do so, he builds an even better suit of armor. However, not everyone in his company likes the new direction he's chosen.",
                    Language = "english",
                    Budget = 150,
                    MovieId = 1,
                    MovieTitle = "Iron Man 1"
                },
                new MovieDetails()
                {
                    Id = 2,
                    Synopsis = "Several months after the events of Iron Man, the film deals with the consequences of Tony Stark outing himself as Iron Man and becoming the world's newest defender. His first major issue is Congressional hearings about sharing his tech, with rival (and perpetually second-place to Tony) industrialist Justin Hammer (Sam Rockwell) standing the most to gain. Despite their best efforts, Tony is untouchable: unbeatable in conferences and unstoppable as Iron Man. But his invincibility is tested by Ivan Vanko/Whiplash (Mickey Rourke), a man with a grudge against the Stark empire who is more than capable of challenging Tony's genius, as Tony is also dealing with a slowly fatal medical condition resulting from his arc reactor implant.",
                    Language = "english",
                    Budget = 170,
                    MovieId = 2,
                    MovieTitle = "Iron Man 2"
                },
                new MovieDetails()
                {
                    Id = 3,
                    Synopsis = "When an enemy from the past targets that which industrialist Tony Stark holds most dear, he must rely on his ingenuity to protect those closest to him. Still haunted by the events from the Battle of New York, he must confront challenges from not only this old adversary but from himself as well, and finally answer a question which has plagued him from the beginning: Is he the one who defines the Iron Man suit? Or is it the suit that defines him?",
                    Language = "english",
                    Budget = 200,
                    MovieId = 3,
                    MovieTitle = "Iron Man 3"
                }
                );
        }
    }
}
