using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public class ActorDto
    {
        [Required(ErrorMessage = "A name is required for the actor.")]
        [MaxLength(50)]
        public string? Name { get; set; }
        public int BirthYear { get; set; }

    }
}
