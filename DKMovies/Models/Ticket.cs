using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ShowTimeID { get; set; }

        [Required]
        public int SeatID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PurchaseTime { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }

        [ForeignKey("ShowTimeID")]
        public ShowTime? ShowTime { get; set; }

        [ForeignKey("SeatID")]
        public Seat? Seat { get; set; }

        public ICollection<TicketPayment> TicketPayments { get; set; } = new List<TicketPayment>();
    }
}
