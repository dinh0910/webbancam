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
    public class SanPhamsController : Controller
    {
        private readonly webbancamContext _context;

        public SanPhamsController(webbancamContext context)
        {
            _context = context;
        }

        // GET: Admin/SanPhams
        public async Task<IActionResult> Index()
        {
            var webbancamContext = _context.SanPham.Include(s => s.DanhMuc).Include(s => s.NhanHieu);
            return View(await webbancamContext.ToListAsync());
        }

        // GET: Admin/SanPhams/Create
        public IActionResult Create()
        {
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "Ten");
            ViewData["NhanHieuID"] = new SelectList(_context.NhanHieu, "NhanHieuID", "Ten");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("SanPhamID,DanhMucID,NhanHieuID,Ten,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.HinhAnh = Upload(file);
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "Ten", sanPham.DanhMucID);
            ViewData["NhanHieuID"] = new SelectList(_context.NhanHieu, "NhanHieuID", "Ten", sanPham.NhanHieuID);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "Ten", sanPham.DanhMucID);
            ViewData["NhanHieuID"] = new SelectList(_context.NhanHieu, "NhanHieuID", "Ten", sanPham.NhanHieuID);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? file, [Bind("SanPhamID,DanhMucID,NhanHieuID,Ten,HinhAnh,DonGia,Sale,ThanhTien,SoLuong")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(file != null)
                    {
                        sanPham.HinhAnh = Upload(file);
                    }
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.SanPhamID))
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
            ViewData["DanhMucID"] = new SelectList(_context.DanhMuc, "DanhMucID", "Ten", sanPham.DanhMucID);
            ViewData["NhanHieuID"] = new SelectList(_context.NhanHieu, "NhanHieuID", "Ten", sanPham.NhanHieuID);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.DanhMuc)
                .Include(s => s.NhanHieu)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SanPham == null)
            {
                return Problem("Entity set 'webbancamContext.SanPham'  is null.");
            }
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPham.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPham?.Any(e => e.SanPhamID == id)).GetValueOrDefault();
        }

        public string Upload(IFormFile file)
        {
            string fn = null;

            if (file != null)
            {
                fn = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\products\\{fn}"; // đường dẫn lưu file
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return fn;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.DanhMuc)
                .Include(s => s.NhanHieu)
                .FirstOrDefaultAsync(m => m.SanPhamID == id);

            ViewBag.thongso = _context.ThongSo;
            ViewBag.hinhanh = _context.HinhAnh;
            ViewBag.mota = _context.MoTa;
            ViewBag.thongtin = _context.ThongTin;

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(IFormFile file, int? id, [Bind("ThongSoID,SanPhamID,TenThongSo,NoiDung")] ThongSo thongSo,
            [Bind("HinhAnhID,SanPhamID,Anh,Active")] HinhAnh hinhAnh, [Bind("MoTaID,SanPhamID,NoiDungMoTa")] MoTa moTa,
            [Bind("ThongSoID,SanPhamID,TrongHop,ChinhSach,BaoHanh")] ThongTin thongTin)
        {
            if (moTa.NoiDungMT != null)
            {
                _context.Update(moTa);
                await _context.SaveChangesAsync();
            }
            else if (thongSo.NoiDung != null || thongSo.TenThongSo != null)
            {
                _context.Update(thongSo);
                await _context.SaveChangesAsync();
            } 
            else if (thongTin.TrongHop != null || thongTin.ChinhSach != null || thongTin.BaoHanh != null)
            {
                _context.Update(thongTin);
                await _context.SaveChangesAsync();
            }
            else
            {
                hinhAnh.Ten = Upload(file);
                _context.Update(hinhAnh);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteMoTa(int? id)
        {
            var tt = await _context.MoTa
                    .FirstOrDefaultAsync(m => m.SanPhamID == id);

            _context.MoTa.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteHinhAnh(int? id)
        {
            var tt = await _context.HinhAnh
                    .FirstOrDefaultAsync(m => m.SanPhamID == id);

            _context.HinhAnh.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteThongSo(int? id, int? idts)
        {
            var tt = await _context.ThongSo
                    .FirstOrDefaultAsync(m => m.ThongSoID == idts);

            _context.ThongSo.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }

        public async Task<IActionResult> DeleteThongTin(int? id, int? idts)
        {
            var tt = await _context.ThongTin
                    .FirstOrDefaultAsync(m => m.ThongTinID == idts);

            _context.ThongTin.Remove(tt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "SanPhams", routeValues: new { id });
        }
    }
}
