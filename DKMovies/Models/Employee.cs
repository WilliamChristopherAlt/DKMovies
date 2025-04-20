using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKMovies.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public int TheaterID { get; set; }

        public int? RoleID { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? CitizenID { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Salary { get; set; }

        [ForeignKey("TheaterID")]
        public virtual Theater? Theater { get; set; }

        [ForeignKey("RoleID")]
        public virtual EmployeeRole? Role { get; set; }
    }
}
