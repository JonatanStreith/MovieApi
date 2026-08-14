using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public record ActorDto
    {
        [Required(ErrorMessage = "An Id is required for the actor.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A name is required for the actor.")]
        [MaxLength(50)]
        public string? Name { get; set; }
        public int BirthYear { get; set; }

    }
}
