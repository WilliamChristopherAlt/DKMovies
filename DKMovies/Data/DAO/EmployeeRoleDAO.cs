using DKMovies.Data;
using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class EmployeeRoleDAO
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRoleDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeRole>> GetAllAsync()
        {
            return await _context.EmployeeRoles.ToListAsync();
        }

        public async Task<EmployeeRole?> GetByIdAsync(int roleId)
        {
            return await _context.EmployeeRoles.FindAsync(roleId);
        }

        public async Task<EmployeeRole?> GetByNameAsync(string roleName)
        {
            return await _context.EmployeeRoles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task AddAsync(EmployeeRole role)
        {
            _context.EmployeeRoles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeRole role)
        {
            _context.EmployeeRoles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.EmployeeRoles.FindAsync(roleId);
            if (role != null)
            {
                _context.EmployeeRoles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int roleId)
        {
            return _context.EmployeeRoles.Any(r => r.RoleID == roleId);
        }

        public bool IsNameUnique(string roleName, int? excludingId = null)
        {
            return !_context.EmployeeRoles.Any(r =>
                r.RoleName == roleName &&
                (!excludingId.HasValue || r.RoleID != excludingId.Value));
        }
    }
}
