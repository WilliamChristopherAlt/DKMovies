using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class AuditoriumDAO
    {
        private readonly ApplicationDbContext _context;

        public AuditoriumDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auditorium>> GetAllAsync()
        {
            return await _context.Auditoriums.Include(a => a.Theater).ToListAsync();
        }

        public async Task<Auditorium?> GetByIdAsync(int id)
        {
            return await _context.Auditoriums.Include(a => a.Theater)
                .FirstOrDefaultAsync(a => a.AuditoriumID == id);
        }

        public async Task AddAsync(Auditorium auditorium)
        {
            _context.Auditoriums.Add(auditorium);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Auditorium auditorium)
        {
            _context.Auditoriums.Update(auditorium);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var auditorium = await _context.Auditoriums.FindAsync(id);
            if (auditorium != null)
            {
                _context.Auditoriums.Remove(auditorium);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Auditoriums.AnyAsync(a => a.AuditoriumID == id);
        }

        public async Task<List<Theater>> GetAllTheatersAsync()
        {
            return await _context.Theaters.ToListAsync();
        }
    }
}
