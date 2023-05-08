using System.ComponentModel.DataAnnotations;

namespace webbancam.Models
{
    public class SanPham
    {
        public int SanPhamID { get; set; }

        public int DanhMucID { get; set; }
        public DanhMuc? DanhMuc { get; set; }

        public int NhanHieuID { get; set; }
        public NhanHieu? NhanHieu { get; set; }
        
        public string? Ten { get; set; }

        public string? HinhAnh { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int DonGia { get; set; }

        public int Sale { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        public int ThanhTien { get; set; }

        public int SoLuong { get; set; }
    }
}
