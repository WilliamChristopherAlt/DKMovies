using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }

        [Required, MaxLength(10)]
        public string RatingValue { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
