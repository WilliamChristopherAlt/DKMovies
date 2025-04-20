using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.BO
{
    public class ShowTimeBO
    {
        private readonly ApplicationDbContext _context;

        public ShowTimeBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowTime>> GetAllAsync()
        {
            return await _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.Auditorium)
                .Include(s => s.SubtitleLanguage)
                .ToListAsync();
        }

        public async Task<ShowTime?> GetByIdAsync(int id)
        {
            return await _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.Auditorium)
                .Include(s => s.SubtitleLanguage)
                .FirstOrDefaultAsync(s => s.ShowTimeID == id);
        }

        public async Task<(bool success, string error)> CreateAsync(ShowTime showTime)
        {
            try
            {
                _context.ShowTimes.Add(showTime);
                await _context.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool success, string error)> UpdateAsync(ShowTime showTime)
        {
            try
            {
                _context.ShowTimes.Update(showTime);
                await _context.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var showTime = await _context.ShowTimes.FindAsync(id);
            if (showTime != null)
            {
                _context.ShowTimes.Remove(showTime);
                await _context.SaveChangesAsync();
            }
        }

        // 🆕 Add these for dropdown population:
        public async Task<List<Auditorium>> GetAuditoriumsAsync()
        {
            return await _context.Auditoriums.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        public async Task<List<Language>> GetLanguagesAsync()
        {
            return await _context.Languages.OrderBy(l => l.LanguageName).ToListAsync();
        }
    }
}
