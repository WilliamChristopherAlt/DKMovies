using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ConcessionID { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PriceAtPurchase { get; set; }

        [ForeignKey("OrderID")]
        public Order? Order { get; set; }

        [ForeignKey("ConcessionID")]
        public Concession? Concession { get; set; }
    }
}
