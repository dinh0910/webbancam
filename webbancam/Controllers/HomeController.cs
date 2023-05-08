using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webbancam.Data;
using webbancam.Models;

namespace webbancam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly webbancamContext _context;

        public HomeController(ILogger<HomeController> logger, webbancamContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var sp = _context.SanPham;
            ViewBag.danhmuc = _context.DanhMuc;
            ViewBag.banner = _context.Banner;
            return View(sp.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}