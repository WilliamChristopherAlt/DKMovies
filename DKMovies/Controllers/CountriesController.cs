using DKMovies.BO;
using DKMovies.DAO;
using DKMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DKMovies.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountryBO _bo;

        public CountriesController(ApplicationDbContext context)
        {
            _bo = new CountryBO(new CountryDAO(context));
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _bo.GetAllCountriesAsync();
            return View(countries);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var country = await _bo.GetCountryByIdAsync(id.Value);
            if (country == null)
                return NotFound();

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,CountryName,Description")] Country country)
        {
            var result = await _bo.AddCountryAsync(country);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var country = await _bo.GetCountryByIdAsync(id.Value);
            if (country == null)
                return NotFound();

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,CountryName,Description")] Country country)
        {
            if (id != country.CountryID)
                return NotFound();

            var result = await _bo.UpdateCountryAsync(country);
            if (result.Success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var country = await _bo.GetCountryByIdAsync(id.Value);
            if (country == null)
                return NotFound();

            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bo.DeleteCountryAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
