using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Models
{
    public class Actor
    {
        public int ActorId { get; set; }

        public string? Name { get; set; }
        public int BirthYear { get; set; }

        public ICollection<MovieActor> MovieActor {  get; set; }
    }
}
