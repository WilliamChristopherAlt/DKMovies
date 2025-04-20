using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; }

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(10)]
        [RegularExpression("Male|Female|Other")]
        public string Gender { get; set; }

        [MaxLength(500)]
        public string? ProfileImagePath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
