using DKMovies.DAO;
using DKMovies.Models;

namespace DKMovies.BO
{
    public class CountryBO
    {
        private readonly CountryDAO _dao;

        public CountryBO(CountryDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> AddCountryAsync(Country country)
        {
            var validation = ValidateCountry(country);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            await _dao.AddAsync(country);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateCountryAsync(Country country)
        {
            var validation = ValidateCountry(country);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            if (!await _dao.ExistsAsync(country.CountryID))
                return (false, "Country not found.");

            await _dao.UpdateAsync(country);
            return (true, string.Empty);
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await _dao.GetByIdAsync(id);
            if (country == null)
                return false;

            await _dao.DeleteAsync(country);
            return true;
        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await _dao.ExistsAsync(id);
        }

        private (bool IsValid, string ErrorMessage) ValidateCountry(Country country)
        {
            if (string.IsNullOrWhiteSpace(country.CountryName))
                return (false, "Country name is required.");

            if (country.CountryName.Length > 100)
                return (false, "Country name must not exceed 100 characters.");

            if (country.Description?.Length > 255)
                return (false, "Description must not exceed 255 characters.");

            return (true, string.Empty);
        }
    }
}
