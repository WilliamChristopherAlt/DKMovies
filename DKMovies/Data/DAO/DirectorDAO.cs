using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class DirectorDAO
    {
        private readonly ApplicationDbContext _context;

        public DirectorDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Director>> GetAllAsync()
        {
            return await _context.Directors.Include(d => d.Country).ToListAsync();
        }

        public async Task<Director?> GetByIdAsync(int id)
        {
            return await _context.Directors.Include(d => d.Country)
                                           .FirstOrDefaultAsync(d => d.DirectorID == id);
        }

        public async Task AddAsync(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Director director)
        {
            _context.Directors.Update(director);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director != null)
            {
                _context.Directors.Remove(director);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Directors.AnyAsync(d => d.DirectorID == id);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }
    }
}
