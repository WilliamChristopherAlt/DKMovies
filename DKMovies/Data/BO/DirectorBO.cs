using DKMovies.DAO;
using DKMovies.Models;

namespace DKMovies.BO
{
    public class DirectorBO
    {
        private readonly DirectorDAO _dao;

        public DirectorBO(DirectorDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Director>> GetAllDirectorsAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Director?> GetDirectorByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _dao.GetAllCountriesAsync();
        }

        public async Task<bool> ValidateAndAddAsync(Director director)
        {
            var validation = ValidateDirector(director);
            if (!validation.isValid)
                throw new Exception(validation.error);

            await _dao.AddAsync(director);
            return true;
        }

        public async Task<bool> ValidateAndUpdateAsync(Director director)
        {
            var validation = ValidateDirector(director);
            if (!validation.isValid)
                throw new Exception(validation.error);

            await _dao.UpdateAsync(director);
            return true;
        }

        public async Task DeleteDirectorAsync(int id)
        {
            await _dao.DeleteAsync(id);
        }

        public async Task<bool> DirectorExistsAsync(int id)
        {
            return await _dao.ExistsAsync(id);
        }

        private (bool isValid, string error) ValidateDirector(Director director)
        {
            if (string.IsNullOrWhiteSpace(director.FullName))
                return (false, "Full name is required.");

            if (director.FullName.Length > 255)
                return (false, "Full name must be 255 characters or less.");

            if (director.DateOfBirth != null && director.DateOfBirth > DateTime.Today)
                return (false, "Date of birth cannot be in the future.");

            if (director.Biography?.Length > 2000)
                return (false, "Biography is too long.");

            if (director.CountryID.HasValue && director.CountryID <= 0)
                return (false, "Invalid country selected.");

            return (true, string.Empty);
        }
    }
}
