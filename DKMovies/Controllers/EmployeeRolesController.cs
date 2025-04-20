using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DKMovies.BO;
using DKMovies.DAO;
using DKMovies.Data;
using DKMovies.Models;

namespace DKMovies.Controllers
{
    public class EmployeeRolesController : Controller
    {
        private readonly EmployeeRoleBO _bo;

        public EmployeeRolesController(ApplicationDbContext context)
        {
            var dao = new EmployeeRoleDAO(context);
            _bo = new EmployeeRoleBO(dao);
        }

        // GET: EmployeeRoles
        public async Task<IActionResult> Index()
        {
            var roles = await _bo.GetAllAsync();
            return View(roles);
        }

        // GET: EmployeeRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await _bo.GetByIdAsync(id.Value);
            if (role == null)
                return NotFound();

            return View(role);
        }

        // GET: EmployeeRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,RoleName,Description")] EmployeeRole role)
        {
            if (!ModelState.IsValid)
                return View(role);

            var (success, error) = await _bo.AddAsync(role);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "An unexpected error occurred.");
                return View(role);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await _bo.GetByIdAsync(id.Value);
            if (role == null)
                return NotFound();

            return View(role);
        }

        // POST: EmployeeRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleID,RoleName,Description")] EmployeeRole role)
        {
            if (id != role.RoleID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(role);

            var (success, error) = await _bo.UpdateAsync(role);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "An unexpected error occurred.");
                return View(role);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await _bo.GetByIdAsync(id.Value);
            if (role == null)
                return NotFound();

            return View(role);
        }

        // POST: EmployeeRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _bo.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
