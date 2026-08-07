using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebAspNet.Data;
using MyWebAspNet.Models;

namespace MyWebAspNet.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser?> GetCurrentUserAsync() => _userManager.GetUserAsync(User);

        private async Task<string?> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Id;
        }

        private async Task<Bill?> FindUserBillAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var userId = await GetCurrentUserIdAsync();
            if (userId == null)
            {
                return null;
            }

            return await _context.Bill
                .Include(b => b.BillCategory)
                .Include(b => b.Currency)
                .Include(b => b.PayAccount)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();
            var billsQuery = _context.Bill
                .Where(b => b.UserId == userId)
                .Include(b => b.BillCategory)
                .Include(b => b.Currency)
                .Include(b => b.PayAccount)
                .Include(b => b.User);

            var bills = await billsQuery.ToListAsync();

            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code");
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name");

            return View(bills);
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var bill = await FindUserBillAsync(id);
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
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PayAccountId,BillCategoryId,CurrencyId,Amount,Note,TransactionDate")] Bill bill)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null)
            {
                return Challenge();
            }

            bill.UserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", bill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", bill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", bill.PayAccountId);
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var bill = await FindUserBillAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", bill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", bill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", bill.PayAccountId);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PayAccountId,BillCategoryId,CurrencyId,Amount,Note,TransactionDate")] Bill updatedBill)
        {
            if (id != updatedBill.Id)
            {
                return NotFound();
            }

            var existingBill = await _context.Bill.FirstOrDefaultAsync(b => b.Id == id);
            var userId = await GetCurrentUserIdAsync();
            if (existingBill == null || existingBill.UserId != userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingBill.PayAccountId = updatedBill.PayAccountId;
                existingBill.BillCategoryId = updatedBill.BillCategoryId;
                existingBill.CurrencyId = updatedBill.CurrencyId;
                existingBill.Amount = updatedBill.Amount;
                existingBill.Note = updatedBill.Note;
                existingBill.TransactionDate = updatedBill.TransactionDate;

                try
                {
                    _context.Update(existingBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(existingBill.Id))
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
            ViewData["BillCategoryId"] = new SelectList(_context.BillCategory, "Id", "Name", updatedBill.BillCategoryId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Code", updatedBill.CurrencyId);
            ViewData["PayAccountId"] = new SelectList(_context.PayAccount, "Id", "Name", updatedBill.PayAccountId);
            return View(updatedBill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var bill = await FindUserBillAsync(id);
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
            var userId = await GetCurrentUserIdAsync();
            var bill = await _context.Bill.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
