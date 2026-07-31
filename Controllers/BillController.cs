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
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bill.Include(b => b.BillCategory).Include(b => b.Currency).Include(b => b.PayAccount).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.BillCategory)
                .Include(b => b.Currency)
                .Include(b => b.PayAccount)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
        {
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PayAccountId,BillCategoryId,CurrencyId,Amount,Note,TransactionDate,CreatedAt,UpdatedAt")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", bill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", bill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", bill.PayAccountId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", bill.UserId);
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", bill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", bill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", bill.PayAccountId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", bill.UserId);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PayAccountId,BillCategoryId,CurrencyId,Amount,Note,TransactionDate,CreatedAt,UpdatedAt")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", bill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", bill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", bill.PayAccountId);
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", bill.UserId);
            return View(bill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.BillCategory)
                .Include(b => b.Currency)
                .Include(b => b.PayAccount)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill != null)
            {
                _context.Bill.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
