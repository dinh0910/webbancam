namespace webbancam.Models
{
    public class SanPham
    {
        public int SanPhamID { get; set; }

        public int DanhMucID { get; set; }
        public DanhMuc? DanhMuc { get; set; }

        public int NhanHieuID { get; set; }
        public NhanHieu? NhanHieu { get; set; }

        public int DonGia { get; set; }

        public int Sale { get; set; }

        public int ThanhTien { get; set; }

        public int SoLuong { get; set; }
    }
}
