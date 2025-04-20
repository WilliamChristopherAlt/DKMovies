using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class ShowTime
    {
        [Key]
        public int ShowTimeID { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        [BindNever]
        public Movie? Movie { get; set; }

        [Required]
        [ForeignKey("Auditorium")]
        public int AuditoriumID { get; set; }
        [BindNever]
        public Auditorium? Auditorium { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        [ForeignKey("Language")]
        public int? SubtitleLanguageID { get; set; }
        [BindNever]
        public Language? SubtitleLanguage { get; set; }

        [Required]
        public bool Is3D { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
