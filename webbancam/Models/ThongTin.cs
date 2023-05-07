namespace webbancam.Models
{
    public class ThongTin
    {
        public int ThongTinID { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public string? TrongHop { get; set; }

        public string? ChinhSach { get; set; }

        public string? BaoHanh { get; set; }
    }
}
