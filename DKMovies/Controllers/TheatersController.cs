using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.BO;
using DKMovies.Models;

namespace DKMovies.Controllers
{
    public class TheatersController : Controller
    {
        private readonly TheaterBO _bo;
        private readonly EmployeeBO _employeeBo;

        public TheatersController(TheaterBO bo, EmployeeBO employeeBo)
        {
            _bo = bo;
            _employeeBo = employeeBo;
        }

        public async Task<IActionResult> Index()
        {
            var theaters = await _bo.GetAllAsync();
            return View(theaters);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var theater = await _bo.GetByIdAsync(id.Value);
            if (theater == null)
                return NotFound();

            return View(theater);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _employeeBo.GetAllAsync();
            ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TheaterID,Name,Location,Phone,ManagerID")] Theater theater)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeBo.GetAllAsync();
                ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email", theater.ManagerID);
                return View(theater);
            }

            var (isValid, errors) = await _bo.CreateAsync(theater);
            if (!isValid)
            {
                foreach (var error in errors)
                    ModelState.AddModelError(string.Empty, error);

                var employees = await _employeeBo.GetAllAsync();
                ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email", theater.ManagerID);
                return View(theater);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var theater = await _bo.GetByIdAsync(id.Value);
            if (theater == null)
                return NotFound();

            var employees = await _employeeBo.GetAllAsync();
            ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email", theater.ManagerID);
            return View(theater);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TheaterID,Name,Location,Phone,ManagerID")] Theater theater)
        {
            if (id != theater.TheaterID)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var employees = await _employeeBo.GetAllAsync();
                ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email", theater.ManagerID);
                return View(theater);
            }

            var (isValid, errors) = await _bo.UpdateAsync(theater);
            if (!isValid)
            {
                foreach (var error in errors)
                    ModelState.AddModelError(string.Empty, error);

                var employees = await _employeeBo.GetAllAsync();
                ViewData["ManagerID"] = new SelectList(employees, "EmployeeID", "Email", theater.ManagerID);
                return View(theater);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var theater = await _bo.GetByIdAsync(id.Value);
            if (theater == null)
                return NotFound();

            return View(theater);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theater = await _bo.GetByIdAsync(id);
            if (theater != null)
                await _bo.DeleteAsync(theater);

            return RedirectToAction(nameof(Index));
        }

        private bool TheaterExists(int id)
        {
            return _bo.Exists(id);
        }
    }
}
