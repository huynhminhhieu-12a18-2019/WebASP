using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class TaiKhoan
    {
        public int TaiKhoanId { get; set; }
        [DisplayName("Tên tài khoản")]
        public string Ten { get; set; }
        [DisplayName("Mật khẩu")]
        public string MK { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Địa chỉ")]
        public string DChi { get; set; }
        [DisplayName("Ngày sinh")]
        public int NgSinh { get; set; }
        [DisplayName("Tháng sinh")]
        public int ThSinh { get; set; }
        [DisplayName("Năm sinh")]
        public int NamSinh { get; set; }
        public string Email { get; set; }
        [DisplayName("Ảnh")]
        public string Anh { get; set; }
        [DisplayName("Ảnh")]
        [NotMapped]
        public IFormFile AnhFile { get; set; }
        public string SDT { get; set; }
        [DisplayName("Loại tài khoản")]
        public int LoaiTKId { get; set; }
        [DisplayName("Loại tài khoản")]
        public LoaiTK LoaiTK { get; set; }
        [DisplayName("Trạng thái")]
        public bool TrangThai { get; set; }
        public List<HoaDon> HoaDons { get; set; }
        public List<GioHang> GioHangs { get; set; }

        public List<BinhLuan> BinhLuans { get; set; }
    }
}
