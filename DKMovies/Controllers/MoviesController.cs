using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.Models;
using DKMovies.DAO;
using DKMovies.BO;

namespace DKMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieBO _bo;
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
            var dao = new MovieDAO(context);
            _bo = new MovieBO(dao);
        }

        private void PopulateDropdowns(Movie movie = null)
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryID", "CountryName", movie?.CountryID);
            ViewData["DirectorID"] = new SelectList(_context.Directors, "DirectorID", "FullName", movie?.DirectorID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "GenreName", movie?.GenreID);
            ViewData["LanguageID"] = new SelectList(_context.Languages, "LanguageID", "LanguageName", movie?.LanguageID);
            ViewData["RatingID"] = new SelectList(_context.Ratings, "RatingID", "RatingValue", movie?.RatingID);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _bo.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _bo.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            var (isValid, errors) = await _bo.AddAsync(movie);
            if (isValid) return RedirectToAction(nameof(Index));

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            PopulateDropdowns(movie);
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _bo.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            PopulateDropdowns(movie);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.MovieID) return NotFound();

            var (isValid, errors) = await _bo.UpdateAsync(movie);
            if (isValid) return RedirectToAction(nameof(Index));

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            PopulateDropdowns(movie);
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _bo.GetByIdAsync(id.Value);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
