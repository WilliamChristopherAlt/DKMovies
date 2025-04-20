using DKMovies.DAO;
using DKMovies.Models;

namespace DKMovies.BO
{
    public class RatingBO
    {
        private readonly RatingDAO _dao;

        public RatingBO(RatingDAO dao)
        {
            _dao = dao;
        }

        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> AddRatingAsync(Rating rating)
        {
            var validation = ValidateRating(rating);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            await _dao.AddAsync(rating);
            return (true, string.Empty);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateRatingAsync(Rating rating)
        {
            var validation = ValidateRating(rating);
            if (!validation.IsValid)
                return (false, validation.ErrorMessage);

            if (!await _dao.ExistsAsync(rating.RatingID))
                return (false, "Rating not found.");

            await _dao.UpdateAsync(rating);
            return (true, string.Empty);
        }

        public async Task<bool> DeleteRatingAsync(int id)
        {
            var rating = await _dao.GetByIdAsync(id);
            if (rating == null)
                return false;

            await _dao.DeleteAsync(rating);
            return true;
        }

        public async Task<bool> RatingExistsAsync(int id)
        {
            return await _dao.ExistsAsync(id);
        }

        private (bool IsValid, string ErrorMessage) ValidateRating(Rating rating)
        {
            if (string.IsNullOrWhiteSpace(rating.RatingValue))
                return (false, "Rating value is required.");

            if (rating.RatingValue.Length > 10)
                return (false, "Rating value must not exceed 10 characters.");

            if (rating.Description?.Length > 255)
                return (false, "Description must not exceed 255 characters.");

            return (true, string.Empty);
        }
    }
}
