using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class MeasurementUnit
    {
        [Key]
        public int UnitID { get; set; }

        [Required]
        [MaxLength(50)]
        public string UnitName { get; set; } = string.Empty;

        public bool IsContinuous { get; set; }

        public ICollection<Concession> Concessions { get; set; } = new List<Concession>();
    }
}
