using DKMovies.BO;
using DKMovies.DAO;
using DKMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DKMovies.Controllers
{
    public class RatingsController : Controller
    {
        private readonly RatingBO _bo;

        public RatingsController(ApplicationDbContext context)
        {
            _bo = new RatingBO(new RatingDAO(context));
        }

        public async Task<IActionResult> Index()
        {
            var ratings = await _bo.GetAllRatingsAsync();
            return View(ratings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var rating = await _bo.GetRatingByIdAsync(id.Value);
            if (rating == null)
                return NotFound();

            return View(rating);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingID,RatingValue,Description")] Rating rating)
        {
            var result = await _bo.AddRatingAsync(rating);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(rating);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var rating = await _bo.GetRatingByIdAsync(id.Value);
            if (rating == null)
                return NotFound();

            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingID,RatingValue,Description")] Rating rating)
        {
            if (id != rating.RatingID)
                return NotFound();

            var result = await _bo.UpdateRatingAsync(rating);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(rating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var rating = await _bo.GetRatingByIdAsync(id.Value);
            if (rating == null)
                return NotFound();

            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bo.DeleteRatingAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
