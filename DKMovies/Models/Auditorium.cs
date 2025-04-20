using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Auditorium
    {
        [Key]
        public int AuditoriumID { get; set; }

        [Required]
        public int TheaterID { get; set; }

        [ForeignKey("TheaterID")]
        public Theater Theater { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
    }
}
