using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }

        [Required]
        public int AuditoriumID { get; set; }

        [ForeignKey("AuditoriumID")]
        public Auditorium Auditorium { get; set; }

        [Required, StringLength(1)]
        public string RowLabel { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
