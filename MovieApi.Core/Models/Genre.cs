using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Models
{
    public record Genre
    {
        public int GenreId { get; set; }

        public string? Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
