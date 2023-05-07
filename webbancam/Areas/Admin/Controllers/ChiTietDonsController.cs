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
    public class ChiTietDonsController : Controller
    {
        private readonly webbancamContext _context;

        public ChiTietDonsController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietDons
        public async Task<IActionResult> Index()
        {
            var webbancamContext = _context.ChiTietDon.Include(c => c.DonDatHang).Include(c => c.SanPham);
            return View(await webbancamContext.ToListAsync());
        }

        // GET: Admin/ChiTietDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChiTietDon == null)
            {
                return NotFound();
            }

            var chiTietDon = await _context.ChiTietDon
                .Include(c => c.DonDatHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietDonID == id);
            if (chiTietDon == null)
            {
                return NotFound();
            }

            return View(chiTietDon);
        }

        // GET: Admin/ChiTietDons/Create
        public IActionResult Create()
        {
            ViewData["DonDatHangID"] = new SelectList(_context.Set<DonDatHang>(), "DonDatHangID", "DonDatHangID");
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID");
            return View();
        }

        // POST: Admin/ChiTietDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietDonID,DonDatHangID,SanPhamID,DonGia,SoLuong,ThanhTien")] ChiTietDon chiTietDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonDatHangID"] = new SelectList(_context.Set<DonDatHang>(), "DonDatHangID", "DonDatHangID", chiTietDon.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", chiTietDon.SanPhamID);
            return View(chiTietDon);
        }

        // GET: Admin/ChiTietDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChiTietDon == null)
            {
                return NotFound();
            }

            var chiTietDon = await _context.ChiTietDon.FindAsync(id);
            if (chiTietDon == null)
            {
                return NotFound();
            }
            ViewData["DonDatHangID"] = new SelectList(_context.Set<DonDatHang>(), "DonDatHangID", "DonDatHangID", chiTietDon.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", chiTietDon.SanPhamID);
            return View(chiTietDon);
        }

        // POST: Admin/ChiTietDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietDonID,DonDatHangID,SanPhamID,DonGia,SoLuong,ThanhTien")] ChiTietDon chiTietDon)
        {
            if (id != chiTietDon.ChiTietDonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonExists(chiTietDon.ChiTietDonID))
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
            ViewData["DonDatHangID"] = new SelectList(_context.Set<DonDatHang>(), "DonDatHangID", "DonDatHangID", chiTietDon.DonDatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.Set<SanPham>(), "SanPhamID", "SanPhamID", chiTietDon.SanPhamID);
            return View(chiTietDon);
        }

        // GET: Admin/ChiTietDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChiTietDon == null)
            {
                return NotFound();
            }

            var chiTietDon = await _context.ChiTietDon
                .Include(c => c.DonDatHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.ChiTietDonID == id);
            if (chiTietDon == null)
            {
                return NotFound();
            }

            return View(chiTietDon);
        }

        // POST: Admin/ChiTietDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChiTietDon == null)
            {
                return Problem("Entity set 'webbancamContext.ChiTietDon'  is null.");
            }
            var chiTietDon = await _context.ChiTietDon.FindAsync(id);
            if (chiTietDon != null)
            {
                _context.ChiTietDon.Remove(chiTietDon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDonExists(int id)
        {
          return (_context.ChiTietDon?.Any(e => e.ChiTietDonID == id)).GetValueOrDefault();
        }
    }
}
