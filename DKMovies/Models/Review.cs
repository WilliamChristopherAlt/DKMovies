using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int MovieID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;

        [ForeignKey("MovieID")]
        public Movie? Movie { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }
    }
}
