using Microsoft.AspNetCore.Mvc;
using DKMovies.BO;
using DKMovies.Models;

namespace DKMovies.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserBO _userBo;

        public UsersController(UserBO userBo)
        {
            _userBo = userBo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userBo.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userBo.GetAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) return View(user);

            var result = await _userBo.CreateAsync(user);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userBo.GetAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserID) return NotFound();
            if (!ModelState.IsValid) return View(user);

            var result = await _userBo.UpdateAsync(user);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userBo.GetAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userBo.DeleteAsync(id);
            if (!result.Success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
