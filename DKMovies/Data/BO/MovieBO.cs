using DKMovies.DAO;
using DKMovies.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DKMovies.BO
{
    public class MovieBO
    {
        private readonly MovieDAO _dao;

        public MovieBO(MovieDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Movie>> GetAllAsync() => await _dao.GetAllAsync();

        public async Task<Movie> GetByIdAsync(int id) => await _dao.GetByIdAsync(id);

        public async Task<bool> ExistsAsync(int id) => await _dao.ExistsAsync(id);

        public async Task<List<string>> ValidateAsync(Movie movie)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(movie.Title))
                errors.Add("Title is required.");
            else if (movie.Title.Length > 255)
                errors.Add("Title must not exceed 255 characters.");

            if (movie.DurationMinutes <= 0)
                errors.Add("Duration must be a positive number.");

            if (movie.ImagePath?.Length > 500)
                errors.Add("Image path must not exceed 500 characters.");

            return errors;
        }

        public async Task<(bool IsValid, List<string> Errors)> AddAsync(Movie movie)
        {
            var errors = await ValidateAsync(movie);
            if (errors.Count > 0)
                return (false, errors);

            await _dao.AddAsync(movie);
            return (true, null);
        }

        public async Task<(bool IsValid, List<string> Errors)> UpdateAsync(Movie movie)
        {
            var errors = await ValidateAsync(movie);
            if (errors.Count > 0)
                return (false, errors);

            await _dao.UpdateAsync(movie);
            return (true, null);
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _dao.GetByIdAsync(id);
            if (movie != null)
                await _dao.DeleteAsync(movie);
        }
    }
}
