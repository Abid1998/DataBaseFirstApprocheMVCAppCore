using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBaseFirstApprocheMVCAppCore.Models;

namespace DataBaseFirstApprocheMVCAppCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly CrudMVCCoreContext _context;

        public StudentController(CrudMVCCoreContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.Emptbl2s != null ? 
                          View(await _context.Emptbl2s.ToListAsync()) :
                          Problem("Entity set 'CrudMVCCoreContext.Emptbl2s'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emptbl2s == null)
            {
                return NotFound();
            }

            var emptbl2 = await _context.Emptbl2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emptbl2 == null)
            {
                return NotFound();
            }

            return View(emptbl2);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone")] Emptbl2 emptbl2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emptbl2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emptbl2);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emptbl2s == null)
            {
                return NotFound();
            }

            var emptbl2 = await _context.Emptbl2s.FindAsync(id);
            if (emptbl2 == null)
            {
                return NotFound();
            }
            return View(emptbl2);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone")] Emptbl2 emptbl2)
        {
            if (id != emptbl2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emptbl2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Emptbl2Exists(emptbl2.Id))
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
            return View(emptbl2);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emptbl2s == null)
            {
                return NotFound();
            }

            var emptbl2 = await _context.Emptbl2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emptbl2 == null)
            {
                return NotFound();
            }

            return View(emptbl2);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emptbl2s == null)
            {
                return Problem("Entity set 'CrudMVCCoreContext.Emptbl2s'  is null.");
            }
            var emptbl2 = await _context.Emptbl2s.FindAsync(id);
            if (emptbl2 != null)
            {
                _context.Emptbl2s.Remove(emptbl2);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Emptbl2Exists(int id)
        {
          return (_context.Emptbl2s?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
