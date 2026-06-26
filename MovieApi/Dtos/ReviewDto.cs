using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public class ReviewDto
    {
        [Required(ErrorMessage = "A name is required for the reviewer.")]
        [MaxLength(50)]
        public string? ReviewerName { get; set; }
        [MaxLength(50)]
        public string? Comment { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }

        public int MovieId { get; set; }
    }
}
