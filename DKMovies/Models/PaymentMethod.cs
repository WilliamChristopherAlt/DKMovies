using DKMovies.Data.BOs;
using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class PaymentMethod
    {
        [Key]
        public int MethodID { get; set; }

        [Required]
        [MaxLength(50)]
        public string MethodName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<TicketPayment> TicketPayments { get; set; } = new List<TicketPayment>();

        public ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
    }
}
