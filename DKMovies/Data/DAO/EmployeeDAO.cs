using DKMovies.Data;
using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class EmployeeDAO
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Theater)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .Include(e => e.Theater)
                .FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        public bool IsEmailUnique(string email, int? excludeId = null)
        {
            return !_context.Employees
                .Any(e => e.Email == email && (!excludeId.HasValue || e.EmployeeID != excludeId.Value));
        }

        public bool IsCitizenIDUnique(string? citizenId, int? excludeId = null)
        {
            if (string.IsNullOrEmpty(citizenId)) return true;

            return !_context.Employees
                .Any(e => e.CitizenID == citizenId && (!excludeId.HasValue || e.EmployeeID != excludeId.Value));
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public bool TheaterExists(int theaterId)
        {
            return _context.Theaters.Any(t => t.TheaterID == theaterId);
        }

        public bool RoleExists(int? roleId)
        {
            return !roleId.HasValue || _context.EmployeeRoles.Any(r => r.RoleID == roleId);
        }
        public async Task<List<Theater>> GetAllTheatersAsync()
        {
            return await _context.Theaters.ToListAsync();
        }
        public async Task<List<EmployeeRole>> GetAllRolesAsync()
        {
            return await _context.EmployeeRoles.ToListAsync();
        }
    }
}
