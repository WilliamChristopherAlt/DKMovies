using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class GenreDAO
    {
        private readonly ApplicationDbContext _context;

        public GenreDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreID == id);
        }
    }
}
