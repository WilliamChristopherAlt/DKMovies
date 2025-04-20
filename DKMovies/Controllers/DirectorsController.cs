using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.Models;
using DKMovies.DAO;
using DKMovies.BO;

namespace DKMovies.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly DirectorBO _bo;

        public DirectorsController(ApplicationDbContext context)
        {
            var dao = new DirectorDAO(context);
            _bo = new DirectorBO(dao);
        }

        public async Task<IActionResult> Index()
        {
            var directors = await _bo.GetAllDirectorsAsync();
            return View(directors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var director = await _bo.GetDirectorByIdAsync(id.Value);
            if (director == null) return NotFound();

            return View(director);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CountryID"] = new SelectList(await _bo.GetAllCountriesAsync(), "CountryID", "CountryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorID,FullName,DateOfBirth,Biography,CountryID")] Director director)
        {
            try
            {
                await _bo.ValidateAndAddAsync(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewData["CountryID"] = new SelectList(await _bo.GetAllCountriesAsync(), "CountryID", "CountryName", director.CountryID);
            return View(director);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var director = await _bo.GetDirectorByIdAsync(id.Value);
            if (director == null) return NotFound();

            ViewData["CountryID"] = new SelectList(await _bo.GetAllCountriesAsync(), "CountryID", "CountryName", director.CountryID);
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DirectorID,FullName,DateOfBirth,Biography,CountryID")] Director director)
        {
            if (id != director.DirectorID) return NotFound();

            try
            {
                await _bo.ValidateAndUpdateAsync(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewData["CountryID"] = new SelectList(await _bo.GetAllCountriesAsync(), "CountryID", "CountryName", director.CountryID);
            return View(director);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var director = await _bo.GetDirectorByIdAsync(id.Value);
            if (director == null) return NotFound();

            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bo.DeleteDirectorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
