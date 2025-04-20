using DKMovies.DAO;
using DKMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class EmployeeRoleBO
    {
        private readonly EmployeeRoleDAO _dao;

        public EmployeeRoleBO(EmployeeRoleDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<EmployeeRole>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<EmployeeRole?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<(bool success, string? error)> AddAsync(EmployeeRole role)
        {
            var validationError = Validate(role, isUpdate: false);
            if (validationError != null)
                return (false, validationError);

            if (!_dao.IsNameUnique(role.RoleName))
                return (false, "Role name must be unique.");

            await _dao.AddAsync(role);
            return (true, null);
        }

        public async Task<(bool success, string? error)> UpdateAsync(EmployeeRole role)
        {
            var validationError = Validate(role, isUpdate: true);
            if (validationError != null)
                return (false, validationError);

            if (!_dao.IsNameUnique(role.RoleName, excludingId: role.RoleID))
                return (false, "Role name must be unique.");

            var existing = await _dao.GetByIdAsync(role.RoleID);
            if (existing == null)
                return (false, "The role you are trying to update does not exist.");

            await _dao.UpdateAsync(role);
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _dao.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _dao.DeleteAsync(id);
            return true;
        }

        private string? Validate(EmployeeRole role, bool isUpdate)
        {
            if (!isUpdate && role.RoleID != 0)
                return "RoleID must not be set manually during creation.";

            if (string.IsNullOrWhiteSpace(role.RoleName))
                return "Role name is required.";

            if (role.RoleName.Length > 100)
                return "Role name must not exceed 100 characters.";

            if (!string.IsNullOrEmpty(role.Description) && role.Description.Length > 255)
                return "Description must not exceed 255 characters.";

            return null;
        }
    }
}
