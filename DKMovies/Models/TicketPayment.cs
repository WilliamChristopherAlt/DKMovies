using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class TicketPayment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int TicketID { get; set; }

        [Required]
        public int MethodID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal? PaidAmount { get; set; }

        public DateTime? PaidAt { get; set; }

        [ForeignKey("TicketID")]
        public Ticket? Ticket { get; set; }

        [ForeignKey("MethodID")]
        public PaymentMethod? PaymentMethod { get; set; }
        public ICollection<TicketPayment> TicketPayments { get; set; } = new List<TicketPayment>();
        public ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
    }
}
