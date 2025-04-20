using System.ComponentModel.DataAnnotations;

namespace DKMovies.Models
{
    public class EmployeeRole
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
