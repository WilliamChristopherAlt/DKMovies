using DKMovies.DAO;
using DKMovies.Models;

namespace DKMovies.BO
{
    public class GenreBO
    {
        private readonly GenreDAO _dao;

        public GenreBO(GenreDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> AddGenreAsync(Genre genre)
        {
            var validation = ValidateGenre(genre);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            await _dao.AddAsync(genre);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateGenreAsync(Genre genre)
        {
            var validation = ValidateGenre(genre);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            if (!await _dao.ExistsAsync(genre.GenreID))
                return (false, "Genre not found.");

            await _dao.UpdateAsync(genre);
            return (true, string.Empty);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _dao.GetByIdAsync(id);
            if (genre == null)
                return false;

            await _dao.DeleteAsync(genre);
            return true;
        }

        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _dao.ExistsAsync(id);
        }

        private (bool IsValid, string ErrorMessage) ValidateGenre(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.GenreName))
                return (false, "Genre name is required.");

            if (genre.GenreName.Length > 100)
                return (false, "Genre name must not exceed 100 characters.");

            if (genre.Description?.Length > 255)
                return (false, "Description must not exceed 255 characters.");

            return (true, string.Empty);
        }
    }
}
