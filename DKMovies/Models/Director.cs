using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Director
    {
        [Key]
        public int DirectorID { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Biography { get; set; }

        [ForeignKey("Country")]
        public int? CountryID { get; set; }
        public Country Country { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
