using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Theater
    {
        [Key]
        public int TheaterID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public int? ManagerID { get; set; }

        [ForeignKey("ManagerID")]
        public virtual Employee? Manager { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();

        public virtual ICollection<Auditorium>? Auditoriums { get; set; } = new List<Auditorium>();
    }
}
