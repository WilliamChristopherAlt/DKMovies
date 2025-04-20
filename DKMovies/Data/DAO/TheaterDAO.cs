using DKMovies.Data;
using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class TheaterDAO
    {
        private readonly ApplicationDbContext _context;

        public TheaterDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Theater>> GetAllAsync() =>
            await _context.Theaters.ToListAsync();

        public async Task<Theater?> GetByIdAsync(int id) =>
            await _context.Theaters
                .Include(t => t.Manager)
                .Include(t => t.Employees)
                .Include(t => t.Auditoriums)
                .FirstOrDefaultAsync(t => t.TheaterID == id);

        public async Task AddAsync(Theater theater)
        {
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Theater theater)
        {
            _context.Theaters.Update(theater);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Theater theater)
        {
            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id) =>
            _context.Theaters.Any(t => t.TheaterID == id);
    }
}
