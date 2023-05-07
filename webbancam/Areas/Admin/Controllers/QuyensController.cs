using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webbancam.Data;
using webbancam.Models;

namespace webbancam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuyensController : Controller
    {
        private readonly webbancamContext _context;

        public QuyensController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/Quyens
        public async Task<IActionResult> Index()
        {
              return _context.Quyen != null ? 
                          View(await _context.Quyen.ToListAsync()) :
                          Problem("Entity set 'webbancamContext.Quyen'  is null.");
        }

        // GET: Admin/Quyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quyen == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyen
                .FirstOrDefaultAsync(m => m.QuyenID == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // GET: Admin/Quyens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Quyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuyenID,Ten")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quyen);
        }

        // GET: Admin/Quyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quyen == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyen.FindAsync(id);
            if (quyen == null)
            {
                return NotFound();
            }
            return View(quyen);
        }

        // POST: Admin/Quyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuyenID,Ten")] Quyen quyen)
        {
            if (id != quyen.QuyenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuyenExists(quyen.QuyenID))
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
            return View(quyen);
        }

        // GET: Admin/Quyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quyen == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyen
                .FirstOrDefaultAsync(m => m.QuyenID == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // POST: Admin/Quyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quyen == null)
            {
                return Problem("Entity set 'webbancamContext.Quyen'  is null.");
            }
            var quyen = await _context.Quyen.FindAsync(id);
            if (quyen != null)
            {
                _context.Quyen.Remove(quyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuyenExists(int id)
        {
          return (_context.Quyen?.Any(e => e.QuyenID == id)).GetValueOrDefault();
        }
    }
}
