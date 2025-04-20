using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [Required, MaxLength(100)]
        public string CountryName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Director> Directors { get; set; } = new List<Director>();
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
