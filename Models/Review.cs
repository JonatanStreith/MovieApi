using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string? ReviewerName { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }

        public int MovieId { get; set; }
        public string? MovieTitle { get; set; } 

    }
}
