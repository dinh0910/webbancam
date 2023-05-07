namespace webbancam.Models
{
    public class HinhAnh
    {
        public int HinhAnhID { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public string? Ten { get; set; }

        public string? Active { get; set; }
    }
}
