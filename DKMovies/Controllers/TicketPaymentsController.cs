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
    public class TicketPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketPayments.Include(t => t.PaymentMethod).Include(t => t.Ticket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPayment = await _context.TicketPayments
                .Include(t => t.PaymentMethod)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (ticketPayment == null)
            {
                return NotFound();
            }

            return View(ticketPayment);
        }

        // GET: TicketPayments/Create
        public IActionResult Create()
        {
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName");
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID");
            return View();
        }

        // POST: TicketPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,TicketID,MethodID,PaymentStatus,PaidAmount,PaidAt")] TicketPayment ticketPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", ticketPayment.MethodID);
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", ticketPayment.TicketID);
            return View(ticketPayment);
        }

        // GET: TicketPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPayment = await _context.TicketPayments.FindAsync(id);
            if (ticketPayment == null)
            {
                return NotFound();
            }
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", ticketPayment.MethodID);
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", ticketPayment.TicketID);
            return View(ticketPayment);
        }

        // POST: TicketPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,TicketID,MethodID,PaymentStatus,PaidAmount,PaidAt")] TicketPayment ticketPayment)
        {
            if (id != ticketPayment.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketPaymentExists(ticketPayment.PaymentID))
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
            ViewData["MethodID"] = new SelectList(_context.PaymentMethods, "MethodID", "MethodName", ticketPayment.MethodID);
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", ticketPayment.TicketID);
            return View(ticketPayment);
        }

        // GET: TicketPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketPayment = await _context.TicketPayments
                .Include(t => t.PaymentMethod)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (ticketPayment == null)
            {
                return NotFound();
            }

            return View(ticketPayment);
        }

        // POST: TicketPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketPayment = await _context.TicketPayments.FindAsync(id);
            if (ticketPayment != null)
            {
                _context.TicketPayments.Remove(ticketPayment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketPaymentExists(int id)
        {
            return _context.TicketPayments.Any(e => e.PaymentID == id);
        }
    }
}
