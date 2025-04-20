using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class ShowTimeDAO
    {
        private readonly ApplicationDbContext _context;

        public ShowTimeDAO(ApplicationDbContext context)
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

        public async Task AddAsync(ShowTime showTime)
        {
            _context.ShowTimes.Add(showTime);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShowTime showTime)
        {
            _context.ShowTimes.Update(showTime);
            await _context.SaveChangesAsync();
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

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ShowTimes.AnyAsync(e => e.ShowTimeID == id);
        }
    }
}
