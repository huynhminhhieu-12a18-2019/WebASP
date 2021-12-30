using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class ChiTietHoaDon
    {
        public int ChiTietHoaDonId { get; set; }
        
        public int HoaDonId { get; set; }
        [DisplayName("Hóa đơn ID")]
        public HoaDon HoaDon { get; set; }
        public int SanPhamId { get; set; }
        [DisplayName("Sản phẩm ID")]
        public SanPham SanPham { get; set; }
        [DisplayName("Số lượng")]
        public int SL { get; set; }
        [DisplayName("Đơn giá")]
        public float DonGia { get; set; }
    }
}
