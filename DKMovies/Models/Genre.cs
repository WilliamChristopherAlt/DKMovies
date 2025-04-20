using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required, MaxLength(100)]
        public string GenreName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
