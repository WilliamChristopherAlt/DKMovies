using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Concession
    {
        [Key]
        public int ConcessionID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockLeft { get; set; }

        [Required]
        public int UnitID { get; set; }

        public bool IsAvailable { get; set; } = true;

        [MaxLength(500)]
        public string? ImagePath { get; set; }

        [ForeignKey("UnitID")]
        public MeasurementUnit? MeasurementUnit { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
