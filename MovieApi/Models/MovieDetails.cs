using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class MovieDetails
    {
        public int MovieDetailsId { get; set; }

        public string? Synopsis { get; set; }
        public string? Language { get; set; }
        public int Budget { get; set; }

        public int? MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public Movie? Movie { get; set; }



    }
}
