using DKMovies.DAO;
using DKMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DKMovies.BO
{
    public class LanguageBO
    {
        private readonly LanguageDAO _languageDAO;

        public LanguageBO(LanguageDAO languageDAO)
        {
            _languageDAO = languageDAO;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _languageDAO.GetAllAsync();
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _languageDAO.GetByIdAsync(id);
        }

        public async Task<string> AddAsync(Language language)
        {
            var validationResult = ValidateLanguage(language);
            if (!string.IsNullOrEmpty(validationResult))
            {
                return validationResult;
            }

            await _languageDAO.AddAsync(language);
            return null;
        }

        public async Task<string> UpdateAsync(Language language)
        {
            var validationResult = ValidateLanguage(language);
            if (!string.IsNullOrEmpty(validationResult))
            {
                return validationResult;
            }

            if (!_languageDAO.LanguageExists(language.LanguageID))
            {
                return "Language not found.";
            }

            await _languageDAO.UpdateAsync(language);
            return null;
        }

        public async Task<string> DeleteAsync(int id)
        {
            if (!_languageDAO.LanguageExists(id))
            {
                return "Language not found.";
            }

            await _languageDAO.DeleteAsync(id);
            return null;
        }

        private string ValidateLanguage(Language language)
        {
            if (string.IsNullOrWhiteSpace(language.LanguageName))
            {
                return "Language name is required.";
            }

            if (language.LanguageName.Length > 100)
            {
                return "Language name cannot exceed 100 characters.";
            }

            if (language.Description?.Length > 500)
            {
                return "Description cannot exceed 500 characters.";
            }

            return null;
        }
    }
}
