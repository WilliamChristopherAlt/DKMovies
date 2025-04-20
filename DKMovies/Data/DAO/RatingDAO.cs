using DKMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace DKMovies.DAO
{
    public class RatingDAO
    {
        private readonly ApplicationDbContext _context;

        public RatingDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rating>> GetAllAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rating rating)
        {
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Ratings.AnyAsync(e => e.RatingID == id);
        }
    }
}
