using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public class MovieDto
    {
        [Required(ErrorMessage = "A name is required for the movie.")]
        [MaxLength(50)]
        public string? Title { get; set; }
        public int Year { get; set; }
        [MaxLength(50)]
        public string? Genre { get; set; }
        public int Duration { get; set; }

        public MovieDetailDto Details { get; set; }



    }
}
