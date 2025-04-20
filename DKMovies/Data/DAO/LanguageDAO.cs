using DKMovies.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DKMovies.DAO
{
    public class LanguageDAO
    {
        private readonly ApplicationDbContext _context;

        public LanguageDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages
                .FirstOrDefaultAsync(l => l.LanguageID == id);
        }

        public async Task AddAsync(Language language)
        {
            _context.Add(language);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language language)
        {
            _context.Update(language);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var language = await GetByIdAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }

        public bool LanguageExists(int id)
        {
            return _context.Languages.Any(l => l.LanguageID == id);
        }
    }
}
