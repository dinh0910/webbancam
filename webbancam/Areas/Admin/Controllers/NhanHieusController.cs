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
    public class NhanHieusController : Controller
    {
        private readonly webbancamContext _context;

        public NhanHieusController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/NhanHieus
        public async Task<IActionResult> Index()
        {
              return _context.NhanHieu != null ? 
                          View(await _context.NhanHieu.ToListAsync()) :
                          Problem("Entity set 'webbancamContext.NhanHieu'  is null.");
        }

        // GET: Admin/NhanHieus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhanHieu == null)
            {
                return NotFound();
            }

            var nhanHieu = await _context.NhanHieu
                .FirstOrDefaultAsync(m => m.NhanHieuID == id);
            if (nhanHieu == null)
            {
                return NotFound();
            }

            return View(nhanHieu);
        }

        // GET: Admin/NhanHieus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanHieus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhanHieuID,Ten")] NhanHieu nhanHieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanHieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanHieu);
        }

        // GET: Admin/NhanHieus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhanHieu == null)
            {
                return NotFound();
            }

            var nhanHieu = await _context.NhanHieu.FindAsync(id);
            if (nhanHieu == null)
            {
                return NotFound();
            }
            return View(nhanHieu);
        }

        // POST: Admin/NhanHieus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhanHieuID,Ten")] NhanHieu nhanHieu)
        {
            if (id != nhanHieu.NhanHieuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanHieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanHieuExists(nhanHieu.NhanHieuID))
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
            return View(nhanHieu);
        }

        // GET: Admin/NhanHieus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhanHieu == null)
            {
                return NotFound();
            }

            var nhanHieu = await _context.NhanHieu
                .FirstOrDefaultAsync(m => m.NhanHieuID == id);
            if (nhanHieu == null)
            {
                return NotFound();
            }

            return View(nhanHieu);
        }

        // POST: Admin/NhanHieus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhanHieu == null)
            {
                return Problem("Entity set 'webbancamContext.NhanHieu'  is null.");
            }
            var nhanHieu = await _context.NhanHieu.FindAsync(id);
            if (nhanHieu != null)
            {
                _context.NhanHieu.Remove(nhanHieu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanHieuExists(int id)
        {
          return (_context.NhanHieu?.Any(e => e.NhanHieuID == id)).GetValueOrDefault();
        }
    }
}
