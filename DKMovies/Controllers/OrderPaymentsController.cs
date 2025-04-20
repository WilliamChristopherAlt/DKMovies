using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DKMovies.Models;

namespace DKMovies.Controllers
{
    public class OrderPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderPayments.Include(o => o.Order).Include(o => o.PaymentMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrderPayments
                .Include(o => o.Order)
                .Include(o => o.PaymentMethod)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (orderPayment == null)
            {
                return NotFound();
            }

            return View(orderPayment);
        }

        // GET: OrderPayments/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID");
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName");
            return View();
        }

        // POST: OrderPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,OrderID,MethodID,PaymentStatus,PaidAmount,PaidAt")] OrderPayment orderPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderPayment.OrderID);
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", orderPayment.MethodID);
            return View(orderPayment);
        }

        // GET: OrderPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrderPayments.FindAsync(id);
            if (orderPayment == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderPayment.OrderID);
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", orderPayment.MethodID);
            return View(orderPayment);
        }

        // POST: OrderPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,OrderID,MethodID,PaymentStatus,PaidAmount,PaidAt")] OrderPayment orderPayment)
        {
            if (id != orderPayment.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPaymentExists(orderPayment.PaymentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "OrderID", orderPayment.OrderID);
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", orderPayment.MethodID);
            return View(orderPayment);
        }

        // GET: OrderPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderPayment = await _context.OrderPayments
                .Include(o => o.Order)
                .Include(o => o.PaymentMethod)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (orderPayment == null)
            {
                return NotFound();
            }

            return View(orderPayment);
        }

        // POST: OrderPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderPayment = await _context.OrderPayments.FindAsync(id);
            if (orderPayment != null)
            {
                _context.OrderPayments.Remove(orderPayment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPaymentExists(int id)
        {
            return _context.OrderPayments.Any(e => e.PaymentID == id);
        }
    }
}
