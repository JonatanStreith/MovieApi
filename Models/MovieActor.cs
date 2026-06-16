using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class MovieActor
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public string? Title { get; set; }
        public int ActorId { get; set; }
        public string? Name { get; set; }


    }
}
