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
    public class BillCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillCategory.ToListAsync());
        }

        // GET: BillCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billCategory = await _context.BillCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billCategory == null)
            {
                return NotFound();
            }

            return View(billCategory);
        }

        // GET: BillCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TransactionType,CreatedAt,UpdatedAt")] BillCategory billCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billCategory);
        }

        // GET: BillCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billCategory = await _context.BillCategory.FindAsync(id);
            if (billCategory == null)
            {
                return NotFound();
            }
            return View(billCategory);
        }

        // POST: BillCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TransactionType,CreatedAt,UpdatedAt")] BillCategory billCategory)
        {
            if (id != billCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillCategoryExists(billCategory.Id))
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
            return View(billCategory);
        }

        // GET: BillCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billCategory = await _context.BillCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billCategory == null)
            {
                return NotFound();
            }

            return View(billCategory);
        }

        // POST: BillCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billCategory = await _context.BillCategory.FindAsync(id);
            if (billCategory != null)
            {
                _context.BillCategory.Remove(billCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillCategoryExists(int id)
        {
            return _context.BillCategory.Any(e => e.Id == id);
        }
    }
}
