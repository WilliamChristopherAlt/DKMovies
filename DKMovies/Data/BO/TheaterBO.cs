using DKMovies.DAO;
using DKMovies.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class TheaterBO
    {
        private readonly TheaterDAO _dao;

        public TheaterBO(TheaterDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Theater>> GetAllAsync() => await _dao.GetAllAsync();
        public async Task<Theater?> GetByIdAsync(int id) => await _dao.GetByIdAsync(id);

        public async Task<(bool isValid, List<string> errors)> CreateAsync(Theater theater)
        {
            var errors = Validate(theater);
            if (errors.Count > 0) return (false, errors);

            await _dao.AddAsync(theater);
            return (true, new List<string>());
        }

        public async Task<(bool isValid, List<string> errors)> UpdateAsync(Theater theater)
        {
            var errors = Validate(theater);
            if (errors.Count > 0) return (false, errors);

            await _dao.UpdateAsync(theater);
            return (true, new List<string>());
        }

        public async Task DeleteAsync(Theater theater) => await _dao.DeleteAsync(theater);
        public bool Exists(int id) => _dao.Exists(id);

        private List<string> Validate(Theater theater)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(theater.Name) || theater.Name.Length > 255)
                errors.Add("Name is required and must be less than or equal to 255 characters.");

            if (string.IsNullOrWhiteSpace(theater.Location) || theater.Location.Length > 255)
                errors.Add("Location is required and must be less than or equal to 255 characters.");

            if (!string.IsNullOrEmpty(theater.Phone) && theater.Phone.Length > 20)
                errors.Add("Phone must be 20 characters or fewer.");

            if (!string.IsNullOrEmpty(theater.Phone) && !Regex.IsMatch(theater.Phone, @"^\+?\d{7,20}$"))
                errors.Add("Phone number is invalid.");

            return errors;
        }
    }
}
