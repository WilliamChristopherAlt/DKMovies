using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class SeatDAO
    {
        private readonly ApplicationDbContext _context;

        public SeatDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seat>> GetAllAsync()
        {
            return await _context.Seats.Include(s => s.Auditorium).ToListAsync();
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            return await _context.Seats.Include(s => s.Auditorium)
                                       .FirstOrDefaultAsync(s => s.SeatID == id);
        }

        public async Task AddAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Seats.AnyAsync(s => s.SeatID == id);
        }

        public async Task<List<Auditorium>> GetAllAuditoriumsAsync()
        {
            return await _context.Auditoriums.ToListAsync();
        }
    }
}
