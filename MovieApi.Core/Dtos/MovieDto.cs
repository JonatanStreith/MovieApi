using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public record MovieDto
    {
        [Required(ErrorMessage = "An id is required for the movie.")]
        public int Id { get; set; }
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
