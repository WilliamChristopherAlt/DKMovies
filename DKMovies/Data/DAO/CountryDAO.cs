using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class CountryDAO
    {
        private readonly ApplicationDbContext _context;

        public CountryDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task AddAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(e => e.CountryID == id);
        }
    }
}
