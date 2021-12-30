using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class GioHang
    {
        public int GioHangId { get; set; }
        public int TaiKhoanId { get; set; }
        [DisplayName("Tài khoản ID")]
        public TaiKhoan TaiKhoan { get; set; }
        public int SanPhamId { get; set; }
        [DisplayName("Sản phẩm ID")]
        public SanPham SanPham { get; set; }
        [DisplayName("Số lượng")]
        public int SL { get; set; }
        [DisplayName("Đơn giá")]
        public float DonGia { get; set; }
        [DisplayName("Tổng tiền")]
        public float TongTien { get; set; }
    }
}
