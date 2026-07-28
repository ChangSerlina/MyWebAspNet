using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebAspNet.Data;
using MyWebAspNet.Models;

namespace MyWebAspNet.Controllers
{
    public class PayAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PayAccount
        public async Task<IActionResult> Index()
        {
            return View(await _context.PayAccount.ToListAsync());
        }

        // GET: PayAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payAccount = await _context.PayAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payAccount == null)
            {
                return NotFound();
            }

            return View(payAccount);
        }

        // GET: PayAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PayAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PayAccount payAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payAccount);
        }

        // GET: PayAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payAccount = await _context.PayAccount.FindAsync(id);
            if (payAccount == null)
            {
                return NotFound();
            }
            return View(payAccount);
        }

        // POST: PayAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PayAccount payAccount)
        {
            if (id != payAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayAccountExists(payAccount.Id))
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
            return View(payAccount);
        }

        // GET: PayAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payAccount = await _context.PayAccount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payAccount == null)
            {
                return NotFound();
            }

            return View(payAccount);
        }

        // POST: PayAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payAccount = await _context.PayAccount.FindAsync(id);
            if (payAccount != null)
            {
                _context.PayAccount.Remove(payAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayAccountExists(int id)
        {
            return _context.PayAccount.Any(e => e.Id == id);
        }
    }
}
