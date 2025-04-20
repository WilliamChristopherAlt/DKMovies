using DKMovies.BO;
using DKMovies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DKMovies.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly LanguageBO _languageBO;

        public LanguagesController(LanguageBO languageBO)
        {
            _languageBO = languageBO;
        }

        // GET: Languages
        public async Task<IActionResult> Index()
        {
            return View(await _languageBO.GetAllAsync());
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _languageBO.GetByIdAsync(id.Value);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageID,LanguageName,Description")] Language language)
        {
            if (ModelState.IsValid)
            {
                var result = await _languageBO.AddAsync(language);
                if (result != null)
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(language);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }

        // GET: Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _languageBO.GetByIdAsync(id.Value);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageID,LanguageName,Description")] Language language)
        {
            if (id != language.LanguageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _languageBO.UpdateAsync(language);
                if (result != null)
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(language);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }

        // GET: Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _languageBO.GetByIdAsync(id.Value);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _languageBO.DeleteAsync(id);
            if (result != null)
            {
                ModelState.AddModelError(string.Empty, result);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
