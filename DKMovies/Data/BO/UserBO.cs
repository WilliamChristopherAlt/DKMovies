using System.ComponentModel.DataAnnotations;
using DKMovies.DAO;
using DKMovies.Models;

namespace DKMovies.BO
{
    public class UserBO
    {
        private readonly UserDAO _userDao;

        public UserBO(UserDAO userDao)
        {
            _userDao = userDao;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userDao.GetAllAsync();
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _userDao.GetByIdAsync(id);
        }

        public async Task<(bool Success, string Message)> CreateAsync(User user)
        {
            var validation = await Validate(user, isNew: true);
            if (!validation.Success)
                return validation;

            user.CreatedAt = DateTime.Now;
            await _userDao.AddAsync(user);
            return (true, "User created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(User user)
        {
            if (!await _userDao.ExistsAsync(user.UserID))
                return (false, "User not found.");

            var validation = await Validate(user, isNew: false);
            if (!validation.Success)
                return validation;

            await _userDao.UpdateAsync(user);
            return (true, "User updated successfully.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            if (!await _userDao.ExistsAsync(id))
                return (false, "User not found.");

            await _userDao.DeleteAsync(id);
            return (true, "User deleted successfully.");
        }

        private async Task<(bool Success, string Message)> Validate(User user, bool isNew)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                return (false, "Username is required.");
            if (string.IsNullOrWhiteSpace(user.Email))
                return (false, "Email is required.");
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                return (false, "Password is required.");

            if (user.Username.Length > 100)
                return (false, "Username must be 100 characters or fewer.");
            if (user.Email.Length > 255)
                return (false, "Email must be 255 characters or fewer.");
            if (user.PasswordHash.Length > 255)
                return (false, "Password must be 255 characters or fewer.");
            if (!string.IsNullOrEmpty(user.FullName) && user.FullName.Length > 255)
                return (false, "Full name must be 255 characters or fewer.");
            if (!string.IsNullOrEmpty(user.Phone) && user.Phone.Length > 20)
                return (false, "Phone must be 20 characters or fewer.");
            if (!string.IsNullOrEmpty(user.ProfileImagePath) && user.ProfileImagePath.Length > 500)
                return (false, "Profile image path must be 500 characters or fewer.");

            if (!string.IsNullOrEmpty(user.Gender) && user.Gender != "Male" && user.Gender != "Female" && user.Gender != "Other")
                return (false, "Gender must be Male, Female, or Other.");

            if (!new EmailAddressAttribute().IsValid(user.Email))
                return (false, "Invalid email format.");

            if (user.BirthDate.HasValue && user.BirthDate.Value > DateTime.Today)
                return (false, "Birth date cannot be in the future.");

            var userByUsername = await _userDao.GetByUsernameAsync(user.Username);
            if (userByUsername != null && (isNew || userByUsername.UserID != user.UserID))
                return (false, "Username already exists.");

            var userByEmail = await _userDao.GetByEmailAsync(user.Email);
            if (userByEmail != null && (isNew || userByEmail.UserID != user.UserID))
                return (false, "Email already exists.");

            return (true, "Validation passed.");
        }
    }
}
