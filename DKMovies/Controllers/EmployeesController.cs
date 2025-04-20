using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DKMovies.BO;
using DKMovies.Models;

namespace DKMovies.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeBO _employeeBO;

        public EmployeesController(EmployeeBO employeeBO)
        {
            _employeeBO = employeeBO;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeBO.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeeBO.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["RoleID"] = new SelectList(await _employeeBO.GetAllRolesAsync(), "RoleID", "RoleName");
            ViewData["TheaterID"] = new SelectList(await _employeeBO.GetAllTheatersAsync(), "TheaterID", "Location");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            var (success, errors) = await _employeeBO.CreateAsync(employee);
            if (!success)
            {
                foreach (var err in errors)
                    ModelState.AddModelError("", err);

                ViewData["RoleID"] = new SelectList(await _employeeBO.GetAllRolesAsync(), "RoleID", "RoleName", employee.RoleID);
                ViewData["TheaterID"] = new SelectList(await _employeeBO.GetAllTheatersAsync(), "TheaterID", "Location", employee.TheaterID);
                return View(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeeBO.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            ViewData["RoleID"] = new SelectList(await _employeeBO.GetAllRolesAsync(), "RoleID", "RoleName", employee.RoleID);
            ViewData["TheaterID"] = new SelectList(await _employeeBO.GetAllTheatersAsync(), "TheaterID", "Location", employee.TheaterID);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID) return NotFound();

            var (success, errors) = await _employeeBO.UpdateAsync(employee);
            if (!success)
            {
                foreach (var err in errors)
                    ModelState.AddModelError("", err);

                ViewData["RoleID"] = new SelectList(await _employeeBO.GetAllRolesAsync(), "RoleID", "RoleName", employee.RoleID);
                ViewData["TheaterID"] = new SelectList(await _employeeBO.GetAllTheatersAsync(), "TheaterID", "Location", employee.TheaterID);
                return View(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _employeeBO.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeBO.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
