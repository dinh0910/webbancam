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
    public class ThongSosController : Controller
    {
        private readonly webbancamContext _context;

        public ThongSosController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/ThongSos
        public async Task<IActionResult> Index()
        {
            var webbancamContext = _context.ThongSo.Include(t => t.SanPham);
            return View(await webbancamContext.ToListAsync());
        }

        // GET: Admin/ThongSos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThongSo == null)
            {
                return NotFound();
            }

            var thongSo = await _context.ThongSo
                .Include(t => t.SanPham)
                .FirstOrDefaultAsync(m => m.ThongSoID == id);
            if (thongSo == null)
            {
                return NotFound();
            }

            return View(thongSo);
        }

        // GET: Admin/ThongSos/Create
        public IActionResult Create()
        {
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/ThongSos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThongSoID,SanPhamID,TenThongSo,NoiDung")] ThongSo thongSo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongSo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongSo.SanPhamID);
            return View(thongSo);
        }

        // GET: Admin/ThongSos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThongSo == null)
            {
                return NotFound();
            }

            var thongSo = await _context.ThongSo.FindAsync(id);
            if (thongSo == null)
            {
                return NotFound();
            }
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongSo.SanPhamID);
            return View(thongSo);
        }

        // POST: Admin/ThongSos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThongSoID,SanPhamID,TenThongSo,NoiDung")] ThongSo thongSo)
        {
            if (id != thongSo.ThongSoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongSo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongSoExists(thongSo.ThongSoID))
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
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongSo.SanPhamID);
            return View(thongSo);
        }

        // GET: Admin/ThongSos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThongSo == null)
            {
                return NotFound();
            }

            var thongSo = await _context.ThongSo
                .Include(t => t.SanPham)
                .FirstOrDefaultAsync(m => m.ThongSoID == id);
            if (thongSo == null)
            {
                return NotFound();
            }

            return View(thongSo);
        }

        // POST: Admin/ThongSos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThongSo == null)
            {
                return Problem("Entity set 'webbancamContext.ThongSo'  is null.");
            }
            var thongSo = await _context.ThongSo.FindAsync(id);
            if (thongSo != null)
            {
                _context.ThongSo.Remove(thongSo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongSoExists(int id)
        {
          return (_context.ThongSo?.Any(e => e.ThongSoID == id)).GetValueOrDefault();
        }
    }
}
