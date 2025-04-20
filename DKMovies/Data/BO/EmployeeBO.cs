using DKMovies.DAO;
using DKMovies.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class EmployeeBO
    {
        private readonly EmployeeDAO _dao;

        public EmployeeBO(EmployeeDAO dao)
        {
            _dao = dao;
        }


        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<List<EmployeeRole>> GetAllRolesAsync()
        {
            return await _dao.GetAllRolesAsync();
        }

        public async Task<List<Theater>> GetAllTheatersAsync()
        {
            return await _dao.GetAllTheatersAsync();
        }

        public async Task<(bool success, List<string> errors)> CreateAsync(Employee employee)
        {
            var errors = Validate(employee, isUpdate: false);
            if (errors.Count > 0) return (false, errors);

            if (!_dao.IsEmailUnique(employee.Email))
                errors.Add("Email must be unique.");

            if (!_dao.IsCitizenIDUnique(employee.CitizenID))
                errors.Add("Citizen ID must be unique.");

            if (!_dao.TheaterExists(employee.TheaterID))
                errors.Add("Assigned theater does not exist.");

            if (!_dao.RoleExists(employee.RoleID))
                errors.Add("Assigned role does not exist.");

            if (errors.Count > 0) return (false, errors);

            await _dao.AddAsync(employee);
            return (true, new List<string>());
        }

        public async Task<(bool success, List<string> errors)> UpdateAsync(Employee employee)
        {
            var errors = Validate(employee, isUpdate: true);
            if (errors.Count > 0) return (false, errors);

            if (!_dao.IsEmailUnique(employee.Email, employee.EmployeeID))
                errors.Add("Email must be unique.");

            if (!_dao.IsCitizenIDUnique(employee.CitizenID, employee.EmployeeID))
                errors.Add("Citizen ID must be unique.");

            if (!_dao.TheaterExists(employee.TheaterID))
                errors.Add("Assigned theater does not exist.");

            if (!_dao.RoleExists(employee.RoleID))
                errors.Add("Assigned role does not exist.");

            if (errors.Count > 0) return (false, errors);

            await _dao.UpdateAsync(employee);
            return (true, new List<string>());
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _dao.GetByIdAsync(id);
            if (emp == null) return false;

            await _dao.DeleteAsync(id);
            return true;
        }

        public List<string> Validate(Employee emp, bool isUpdate)
        {
            var errors = new List<string>();

            if (!isUpdate && emp.EmployeeID != 0)
                errors.Add("EmployeeID should not be set during creation.");

            if (string.IsNullOrWhiteSpace(emp.FullName) || emp.FullName.Length > 255)
                errors.Add("Full name is required and must not exceed 255 characters.");

            if (string.IsNullOrWhiteSpace(emp.Email) || emp.Email.Length > 255)
                errors.Add("Email is required and must not exceed 255 characters.");
            else if (!Regex.IsMatch(emp.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Email format is invalid.");

            if (emp.Phone?.Length > 20)
                errors.Add("Phone number must not exceed 20 characters.");

            if (emp.Gender != null && emp.Gender != "Male" && emp.Gender != "Female" && emp.Gender != "Other")
                errors.Add("Gender must be Male, Female, or Other.");

            if (emp.CitizenID?.Length > 50)
                errors.Add("Citizen ID must not exceed 50 characters.");

            if (emp.Address?.Length > 255)
                errors.Add("Address must not exceed 255 characters.");

            if (emp.Salary < 0 || emp.Salary > 100000000)
                errors.Add("Salary must be a positive value and realistic.");

            return errors;
        }
    }
}
