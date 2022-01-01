using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class SanPham
    {
        [DisplayName("STT")]
        public int SanPhamId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string MASP { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }
        [DisplayName("Loại sản phẩm")]
        public int LoaiSPId { get; set; }
        [DisplayName("Loại sản phẩm")]
        public LoaiSP LoaiSP { get; set; }
        [DisplayName("Giá bán")]
        public float DonGia { get; set; }
        [DisplayName("Ảnh minh họa")]
        public string Anh { get; set; }

        [DisplayName("Ảnh minh họa")]
        [NotMapped]
        public IFormFile AnhFile { get; set; }
        [DisplayName("Số lượng")]
        public int SL { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [DisplayName("Trạng thái")]
        public bool Trangthai { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public List<GioHang> GioHangs { get; set; }
    }
}
