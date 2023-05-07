using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webbancam.Models;

namespace webbancam.Data
{
    public class webbancamContext : DbContext
    {
        public webbancamContext (DbContextOptions<webbancamContext> options)
            : base(options)
        {
        }

        public DbSet<webbancam.Models.Banner> Banner { get; set; } = default!;

        public DbSet<webbancam.Models.Quyen>? Quyen { get; set; }

        public DbSet<webbancam.Models.TaiKhoan>? TaiKhoan { get; set; }

        public DbSet<webbancam.Models.ChiTietDon>? ChiTietDon { get; set; }

        public DbSet<webbancam.Models.DanhMuc>? DanhMuc { get; set; }

        public DbSet<webbancam.Models.DonDatHang>? DonDatHang { get; set; }

        public DbSet<webbancam.Models.HinhAnh>? HinhAnh { get; set; }

        public DbSet<webbancam.Models.MoTa>? MoTa { get; set; }

        public DbSet<webbancam.Models.NhanHieu>? NhanHieu { get; set; }

        public DbSet<webbancam.Models.SanPham>? SanPham { get; set; }

        public DbSet<webbancam.Models.ThongSo>? ThongSo { get; set; }

        public DbSet<webbancam.Models.ThongTin>? ThongTin { get; set; }
    }
}
