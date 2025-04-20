using DKMovies.BO;
using DKMovies.DAO;
using DKMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DKMovies.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenreBO _bo;

        public GenresController(ApplicationDbContext context)
        {
            _bo = new GenreBO(new GenreDAO(context));
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _bo.GetAllGenresAsync();
            return View(genres);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var genre = await _bo.GetGenreByIdAsync(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreID,GenreName,Description")] Genre genre)
        {
            var result = await _bo.AddGenreAsync(genre);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(genre);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var genre = await _bo.GetGenreByIdAsync(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreID,GenreName,Description")] Genre genre)
        {
            if (id != genre.GenreID)
                return NotFound();

            var result = await _bo.UpdateGenreAsync(genre);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(genre);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var genre = await _bo.GetGenreByIdAsync(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bo.DeleteGenreAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
