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
    public class ThongTinsController : Controller
    {
        private readonly webbancamContext _context;

        public ThongTinsController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/ThongTins
        public async Task<IActionResult> Index()
        {
            var webbancamContext = _context.ThongTin.Include(t => t.SanPham);
            return View(await webbancamContext.ToListAsync());
        }

        // GET: Admin/ThongTins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThongTin == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTin
                .Include(t => t.SanPham)
                .FirstOrDefaultAsync(m => m.ThongTinID == id);
            if (thongTin == null)
            {
                return NotFound();
            }

            return View(thongTin);
        }

        // GET: Admin/ThongTins/Create
        public IActionResult Create()
        {
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/ThongTins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThongTinID,SanPhamID,TrongHop,ChinhSach,BaoHanh")] ThongTin thongTin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongTin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongTin.SanPhamID);
            return View(thongTin);
        }

        // GET: Admin/ThongTins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThongTin == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTin.FindAsync(id);
            if (thongTin == null)
            {
                return NotFound();
            }
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongTin.SanPhamID);
            return View(thongTin);
        }

        // POST: Admin/ThongTins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThongTinID,SanPhamID,TrongHop,ChinhSach,BaoHanh")] ThongTin thongTin)
        {
            if (id != thongTin.ThongTinID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongTin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongTinExists(thongTin.ThongTinID))
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
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "SanPhamID", thongTin.SanPhamID);
            return View(thongTin);
        }

        // GET: Admin/ThongTins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThongTin == null)
            {
                return NotFound();
            }

            var thongTin = await _context.ThongTin
                .Include(t => t.SanPham)
                .FirstOrDefaultAsync(m => m.ThongTinID == id);
            if (thongTin == null)
            {
                return NotFound();
            }

            return View(thongTin);
        }

        // POST: Admin/ThongTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThongTin == null)
            {
                return Problem("Entity set 'webbancamContext.ThongTin'  is null.");
            }
            var thongTin = await _context.ThongTin.FindAsync(id);
            if (thongTin != null)
            {
                _context.ThongTin.Remove(thongTin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongTinExists(int id)
        {
          return (_context.ThongTin?.Any(e => e.ThongTinID == id)).GetValueOrDefault();
        }
    }
}
