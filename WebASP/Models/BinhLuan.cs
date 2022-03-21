using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class BinhLuan
    {
        public int BinhLuanId { get; set; }
        [DisplayName("Nội dung")]
        public string noidung { get; set; }
        [DisplayName("Trạng thái")]
        public bool trangthai { get; set; }
        [DisplayName("Thời gian")]
        public DateTime thoigian { get; set; }
        public int TaiKhoanId { get; set; }
        [DisplayName("Tài khoản")]
        public TaiKhoan TaiKhoan { get; set; }
        public int SanPhamId { get; set; }
        [DisplayName("Sản phẩm")]
        public SanPham SanPham { get; set; }
    }
}
