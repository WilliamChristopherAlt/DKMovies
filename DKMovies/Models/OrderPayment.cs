using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class OrderPayment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int MethodID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal? PaidAmount { get; set; }

        public DateTime? PaidAt { get; set; }

        [ForeignKey("OrderID")]
        public Order? Order { get; set; }

        [ForeignKey("MethodID")]
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
