using DKMovies.DAO;
using DKMovies.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class AuditoriumBO
    {
        private readonly AuditoriumDAO _dao;

        public AuditoriumBO(AuditoriumDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Auditorium>> GetAllAsync() => await _dao.GetAllAsync();

        public async Task<Auditorium?> GetByIdAsync(int id) => await _dao.GetByIdAsync(id);

        public async Task<List<Theater>> GetAllTheatersAsync() => await _dao.GetAllTheatersAsync();

        public async Task<(bool isValid, List<string> errors)> ValidateAsync(Auditorium auditorium)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(auditorium.Name))
                errors.Add("Auditorium name is required.");
            else if (auditorium.Name.Length > 50)
                errors.Add("Auditorium name must be at most 50 characters.");

            if (auditorium.Capacity <= 0)
                errors.Add("Capacity must be a positive number.");

            var theaters = await _dao.GetAllTheatersAsync();
            if (!theaters.Exists(t => t.TheaterID == auditorium.TheaterID))
                errors.Add("Specified Theater does not exist.");

            return (errors.Count == 0, errors);
        }

        public async Task<bool> AddAsync(Auditorium auditorium)
        {
            var (isValid, _) = await ValidateAsync(auditorium);
            if (!isValid) return false;

            await _dao.AddAsync(auditorium);
            return true;
        }

        public async Task<bool> UpdateAsync(Auditorium auditorium)
        {
            var (isValid, _) = await ValidateAsync(auditorium);
            if (!isValid) return false;

            await _dao.UpdateAsync(auditorium);
            return true;
        }

        public async Task DeleteAsync(int id) => await _dao.DeleteAsync(id);

        public async Task<bool> ExistsAsync(int id) => await _dao.ExistsAsync(id);
    }
}
