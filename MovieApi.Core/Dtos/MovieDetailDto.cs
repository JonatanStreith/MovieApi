using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public record MovieDetailDto
    {
        [Required(ErrorMessage = "No Id for related movie provided.")]
        public int? MovieId { get; set; }
        [MaxLength(2500)]
        public string? Synopsis { get; set; }
        [MaxLength(50)]
        public string? Language { get; set; }
        [Range(0, int.MaxValue)]
        public int Budget { get; set; }

        public List<ReviewDto> Reviews { get; set; }
        public List<ActorDto> Actors { get; set; }
    }
}
