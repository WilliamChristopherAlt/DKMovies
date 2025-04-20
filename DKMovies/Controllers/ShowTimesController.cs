using DKMovies.BO;
using DKMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DKMovies.Controllers
{
    public class ShowTimesController : Controller
    {
        private readonly ShowTimeBO _bo;

        public ShowTimesController(ApplicationDbContext context)
        {
            _bo = new ShowTimeBO(context);
        }

        // GET: ShowTimes
        public async Task<IActionResult> Index()
        {
            var showTimes = await _bo.GetAllAsync();
            return View(showTimes);
        }

        // GET: ShowTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var showTime = await _bo.GetByIdAsync(id.Value);
            if (showTime == null)
                return NotFound();

            return View(showTime);
        }

        // GET: ShowTimes/Create
        public async Task<IActionResult> Create()
        {
            await PopulateSelectListsAsync();
            return View();
        }

        // POST: ShowTimes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,AuditoriumID,StartTime,DurationMinutes,SubtitleLanguageID,Is3D")] ShowTime showTime)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectListsAsync(showTime);
                return View(showTime);
            }

            var (success, error) = await _bo.CreateAsync(showTime);
            if (success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, error);
            await PopulateSelectListsAsync(showTime);
            return View(showTime);
        }

        // GET: ShowTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var showTime = await _bo.GetByIdAsync(id.Value);
            if (showTime == null)
                return NotFound();

            await PopulateSelectListsAsync(showTime);
            return View(showTime);
        }

        // POST: ShowTimes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShowTimeID,MovieID,AuditoriumID,StartTime,DurationMinutes,SubtitleLanguageID,Is3D")] ShowTime showTime)
        {
            if (id != showTime.ShowTimeID)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateSelectListsAsync(showTime);
                return View(showTime);
            }

            var (success, error) = await _bo.UpdateAsync(showTime);
            if (success)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, error);
            await PopulateSelectListsAsync(showTime);
            return View(showTime);
        }

        // GET: ShowTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var showTime = await _bo.GetByIdAsync(id.Value);
            if (showTime == null)
                return NotFound();

            return View(showTime);
        }

        // POST: ShowTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateSelectListsAsync(ShowTime? showTime = null)
        {
            var auditoriums = await _bo.GetAuditoriumsAsync();
            var movies = await _bo.GetMoviesAsync();
            var languages = await _bo.GetLanguagesAsync();

            ViewData["AuditoriumID"] = new SelectList(auditoriums, "AuditoriumID", "Name", showTime?.AuditoriumID);
            ViewData["MovieID"] = new SelectList(movies, "MovieID", "Title", showTime?.MovieID);
            ViewData["SubtitleLanguageID"] = new SelectList(languages, "LanguageID", "LanguageName", showTime?.SubtitleLanguageID);
        }
    }
}
