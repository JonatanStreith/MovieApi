using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }
        public int Year {  get; set; }
        public string? Genre { get; set; }
        public int Duration { get; set; }

        public int MovieDetailsId { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<MovieActor> MovieActor { get; set; }


    }
}
