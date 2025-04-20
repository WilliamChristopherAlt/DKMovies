using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.BO;
using DKMovies.Models;
using DKMovies.DAO;

namespace DKMovies.Controllers
{
    public class SeatsController : Controller
    {
        private readonly SeatBO _bo;

        public SeatsController(ApplicationDbContext context)
        {
            var dao = new SeatDAO(context);
            _bo = new SeatBO(dao);
        }

        public async Task<IActionResult> Index()
        {
            var seats = await _bo.GetAllAsync();
            return View(seats);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var seat = await _bo.GetByIdAsync(id.Value);
            if (seat == null) return NotFound();

            return View(seat);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["AuditoriumID"] = new SelectList(await _bo.GetAllAuditoriumsAsync(), "AuditoriumID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeatID,AuditoriumID,RowLabel,SeatNumber")] Seat seat)
        {
            var (isValid, errors) = await _bo.ValidateAsync(seat);
            if (isValid)
            {
                await _bo.AddAsync(seat);
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            ViewData["AuditoriumID"] = new SelectList(await _bo.GetAllAuditoriumsAsync(), "AuditoriumID", "Name", seat.AuditoriumID);
            return View(seat);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var seat = await _bo.GetByIdAsync(id.Value);
            if (seat == null) return NotFound();

            ViewData["AuditoriumID"] = new SelectList(await _bo.GetAllAuditoriumsAsync(), "AuditoriumID", "Name", seat.AuditoriumID);
            return View(seat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeatID,AuditoriumID,RowLabel,SeatNumber")] Seat seat)
        {
            if (id != seat.SeatID) return NotFound();

            var (isValid, errors) = await _bo.ValidateAsync(seat);
            if (isValid)
            {
                await _bo.UpdateAsync(seat);
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            ViewData["AuditoriumID"] = new SelectList(await _bo.GetAllAuditoriumsAsync(), "AuditoriumID", "Name", seat.AuditoriumID);
            return View(seat);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var seat = await _bo.GetByIdAsync(id.Value);
            if (seat == null) return NotFound();

            return View(seat);
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
