using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        [ForeignKey("Genre")]
        public int? GenreID { get; set; }
        public Genre Genre { get; set; }

        [ForeignKey("Rating")]
        public int? RatingID { get; set; }
        public Rating Rating { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [ForeignKey("Language")]
        public int? LanguageID { get; set; }
        public Language Language { get; set; }

        [ForeignKey("Country")]
        public int? CountryID { get; set; }
        public Country Country { get; set; }

        [ForeignKey("Director")]
        public int? DirectorID { get; set; }
        public Director Director { get; set; }

        [MaxLength(500)]
        public string? ImagePath { get; set; }

        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
