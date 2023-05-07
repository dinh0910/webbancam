namespace webbancam.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanID { get; set; }

        public int QuyenID { get; set; }
        public Quyen? Quyen { get; set; }

        public string? TenTaiKhoan { get; set; }

        public string? MatKhau { get; set; }

        public string? HoTen { get; set; }

        public string? Sdt { get; set; }

        public string? DiaChi { get; set; }
    }
}
