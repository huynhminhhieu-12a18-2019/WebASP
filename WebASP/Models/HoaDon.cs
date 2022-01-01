using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class HoaDon
    {
        public int HoaDonId { get; set; }
        [DisplayName("Mã hóa đơn")]
        public string MAHD { get; set; }
        public int TaiKhoanId { get; set; }
        [DisplayName("Tên người mua")]
        public TaiKhoan TaiKhoan { get; set; }
        [DisplayName("Ngày lập")]
        public DateTime NgayLap { get; set; }
        [DisplayName("Thanh toán")]
        public bool ThanhToan { get; set; }
        [DisplayName("Địa chỉ giao hàng")]
        public string DChiGiaoHang { get; set; }
        [DisplayName("SDT giao hàng")]
        public string SDTGiaoHang { get; set; }
        [DisplayName("Tên người nhận")]
        public string TenNguoiNhan { get; set; }
        [DisplayName("Tổng tiền")]
        public float TongTien { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
