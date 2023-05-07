namespace webbancam.Models
{
    public class ChiTietDon
    {
        public int ChiTietDonID { get; set; }

        public int DonDatHangID { get; set; }
        public DonDatHang? DonDatHang { get; set; }

        public int SanPhamID { get; set; }
        public SanPham? SanPham { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        public int ThanhTien { get; set; }
    }
}
