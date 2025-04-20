using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }

        [Required, MaxLength(100)]
        public string LanguageName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<ShowTime> Showtimes { get; set; } = new List<ShowTime>();
    }
}
