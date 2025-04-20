using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.BO;
using DKMovies.Models;
using DKMovies.DAO;

namespace DKMovies.Controllers
{
    public class AuditoriumsController : Controller
    {
        private readonly AuditoriumBO _bo;

        public AuditoriumsController(ApplicationDbContext context)
        {
            var dao = new AuditoriumDAO(context);
            _bo = new AuditoriumBO(dao);
        }

        public async Task<IActionResult> Index()
        {
            var auditoriums = await _bo.GetAllAsync();
            return View(auditoriums);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var auditorium = await _bo.GetByIdAsync(id.Value);
            if (auditorium == null) return NotFound();

            return View(auditorium);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["TheaterID"] = new SelectList(await _bo.GetAllTheatersAsync(), "TheaterID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuditoriumID,TheaterID,Name,Capacity")] Auditorium auditorium)
        {
            var (isValid, errors) = await _bo.ValidateAsync(auditorium);
            if (isValid)
            {
                await _bo.AddAsync(auditorium);
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            ViewData["TheaterID"] = new SelectList(await _bo.GetAllTheatersAsync(), "TheaterID", "Name", auditorium.TheaterID);
            return View(auditorium);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var auditorium = await _bo.GetByIdAsync(id.Value);
            if (auditorium == null) return NotFound();

            ViewData["TheaterID"] = new SelectList(await _bo.GetAllTheatersAsync(), "TheaterID", "Name", auditorium.TheaterID);
            return View(auditorium);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuditoriumID,TheaterID,Name,Capacity")] Auditorium auditorium)
        {
            if (id != auditorium.AuditoriumID) return NotFound();

            var (isValid, errors) = await _bo.ValidateAsync(auditorium);
            if (isValid)
            {
                await _bo.UpdateAsync(auditorium);
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            ViewData["TheaterID"] = new SelectList(await _bo.GetAllTheatersAsync(), "TheaterID", "Name", auditorium.TheaterID);
            return View(auditorium);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var auditorium = await _bo.GetByIdAsync(id.Value);
            if (auditorium == null) return NotFound();

            return View(auditorium);
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
