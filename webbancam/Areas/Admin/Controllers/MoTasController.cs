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
    public class MoTasController : Controller
    {
        private readonly webbancamContext _context;

        public MoTasController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/MoTas
        public async Task<IActionResult> Index()
        {
            var webbancamContext = _context.MoTa.Include(m => m.SanPham);
            return View(await webbancamContext.ToListAsync());
        }

        // GET: Admin/MoTas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MoTa == null)
            {
                return NotFound();
            }

            var moTa = await _context.MoTa
                .Include(m => m.SanPham)
                .FirstOrDefaultAsync(m => m.MoTaID == id);
            if (moTa == null)
            {
                return NotFound();
            }

            return View(moTa);
        }

        // GET: Admin/MoTas/Create
        public IActionResult Create()
        {
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/MoTas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoTaID,SanPhamID,NoiDungMT")] MoTa moTa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moTa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", moTa.SanPhamID);
            return View(moTa);
        }

        // GET: Admin/MoTas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MoTa == null)
            {
                return NotFound();
            }

            var moTa = await _context.MoTa.FindAsync(id);
            if (moTa == null)
            {
                return NotFound();
            }
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", moTa.SanPhamID);
            return View(moTa);
        }

        // POST: Admin/MoTas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoTaID,SanPhamID,NoiDungMT")] MoTa moTa)
        {
            if (id != moTa.MoTaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moTa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoTaExists(moTa.MoTaID))
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
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", moTa.SanPhamID);
            return View(moTa);
        }

        // GET: Admin/MoTas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MoTa == null)
            {
                return NotFound();
            }

            var moTa = await _context.MoTa
                .Include(m => m.SanPham)
                .FirstOrDefaultAsync(m => m.MoTaID == id);
            if (moTa == null)
            {
                return NotFound();
            }

            return View(moTa);
        }

        // POST: Admin/MoTas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MoTa == null)
            {
                return Problem("Entity set 'webbancamContext.MoTa'  is null.");
            }
            var moTa = await _context.MoTa.FindAsync(id);
            if (moTa != null)
            {
                _context.MoTa.Remove(moTa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoTaExists(int id)
        {
          return (_context.MoTa?.Any(e => e.MoTaID == id)).GetValueOrDefault();
        }
    }
}
