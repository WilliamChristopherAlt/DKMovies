using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class MovieDAO
    {
        private readonly ApplicationDbContext _context;

        public MovieDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Country)
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.Language)
                .Include(m => m.Rating)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Country)
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.Language)
                .Include(m => m.Rating)
                .FirstOrDefaultAsync(m => m.MovieID == id);
        }

        public async Task AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(e => e.MovieID == id);
        }
    }
}
