using DKMovies.DAO;
using DKMovies.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class SeatBO
    {
        private readonly SeatDAO _dao;

        public SeatBO(SeatDAO dao)
        {
            _dao = dao;
        }

        public Task<List<Seat>> GetAllAsync() => _dao.GetAllAsync();

        public Task<Seat> GetByIdAsync(int id) => _dao.GetByIdAsync(id);

        public Task<List<Auditorium>> GetAllAuditoriumsAsync() => _dao.GetAllAuditoriumsAsync();

        public async Task AddAsync(Seat seat) => await _dao.AddAsync(seat);

        public async Task UpdateAsync(Seat seat) => await _dao.UpdateAsync(seat);

        public async Task DeleteAsync(int id) => await _dao.DeleteAsync(id);

        public async Task<(bool IsValid, List<string> Errors)> ValidateAsync(Seat seat)
        {
            var errors = new List<string>();

            if (seat.AuditoriumID <= 0)
                errors.Add("Auditorium must be selected.");

            if (string.IsNullOrWhiteSpace(seat.RowLabel) || seat.RowLabel.Length != 1)
                errors.Add("Row label must be a single character.");

            if (seat.SeatNumber <= 0)
                errors.Add("Seat number must be greater than 0.");

            var auditoriums = await _dao.GetAllAuditoriumsAsync();
            if (!auditoriums.Any(a => a.AuditoriumID == seat.AuditoriumID))
                errors.Add("Selected auditorium does not exist.");

            return (errors.Count == 0, errors);
        }
    }
}
