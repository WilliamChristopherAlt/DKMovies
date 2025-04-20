using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        [ForeignKey("UserID")]
        public User? User { get; set; }
        public ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
