using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebASPContext _context;

        public HomeController(WebASPContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            ViewBag.TaiKhoanid =  Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP);
            return View(sanphams);
        }
        public IActionResult AllProducts()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP);
            return View(sanphams);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string tk, string mk)
        {
            TaiKhoan taikhoan = _context.TaiKhoans.Include(tkhoan=>tkhoan.LoaiTK).Where(tkhoan => tkhoan.Ten == tk && tkhoan.MK == mk).FirstOrDefault();
            if (taikhoan != null)
            {
                CookieOptions cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                HttpContext.Response.Cookies.Append("TaiKhoanId", taikhoan.TaiKhoanId.ToString(), cookieOptions);
                HttpContext.Response.Cookies.Append("HoTen", taikhoan.HoTen.ToString(), cookieOptions);

                //HttpContext.Session.SetInt32("TaiKhoanId", taikhoan.TaiKhoanId);
                //HttpContext.Session.SetString("HoTen", taikhoan.HoTen.ToString());
                if (taikhoan.LoaiTKId != 0)
                {
                    if (taikhoan.LoaiTKId == 1)
                    {
                        return RedirectToAction("Index", "Admins");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }    
            ViewBag.dangnhapthatbai = "Tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("HoTen", "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            HttpContext.Response.Cookies.Append("TaiKhoanId", "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            //HttpContext.Session.Remove("HoTen");
            //HttpContext.Session.Remove("TaiKhoanId");
            return RedirectToAction("Login", "Home");
        }
        public IActionResult SingleProduct(int id)
        {
            var sanPham = _context.SanPhams.Include(sp => sp.LoaiSP).Where(sp => sp.SanPhamId == id).FirstOrDefault();
            List<SanPham> sanphams = _context.SanPhams.Where(sp => sp.LoaiSP.TenLoai == sanPham.LoaiSP.TenLoai).ToList();
            ViewBag.sanphamtuongtu = sanphams;
            return View(sanPham);
        }
        public IActionResult Cart()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                return RedirectToAction("Login", "Home");
            }
            
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            List<GioHang> giohang = _context.GioHangs.Include(gio=>gio.SanPham).Where(gio => gio.TaiKhoanId == taikhoanid).ToList();
            float tongtien = 0;
            foreach(var gio in giohang)
            {
                tongtien += gio.TongTien;
            }
            ViewBag.tongtien = tongtien;
            ViewBag.slsp = giohang.Count();
            return View(giohang);
        }
        public IActionResult Addcart(int id)
        {
            return Addcart(id,1);
        }

        [HttpPost]
        public IActionResult Addcart(int sanphamId, int sl)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                return RedirectToAction("Login", "Home");
            }
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            GioHang giohang = _context.GioHangs.Include(gio=>gio.SanPham).FirstOrDefault(gio => gio.TaiKhoanId == taikhoanid && gio.SanPhamId == sanphamId);
            float tongtien = _context.SanPhams.Where(sp => sp.SanPhamId == sanphamId).Select(sp => sp.DonGia).FirstOrDefault();
            if (giohang == null)
            {
                giohang = new GioHang();
                giohang.TaiKhoanId = taikhoanid;
                giohang.SanPhamId = sanphamId;
                giohang.SL = sl;
                giohang.TongTien = tongtien;
                _context.GioHangs.Add(giohang);
            }
            else
            {
                giohang.SL += sl;
                giohang.TongTien += sl * giohang.SanPham.DonGia;
            }
            _context.SaveChanges();
            return RedirectToAction("Cart", "Home");
        }
        [HttpPost]
        public IActionResult CapNhatSL(int id, int quantity)
        {
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            GioHang giohang = _context.GioHangs.Include(gio => gio.SanPham).FirstOrDefault(gio => gio.TaiKhoanId == taikhoanid && gio.SanPhamId == id);
            giohang.SL = quantity;
            giohang.TongTien = quantity * giohang.SanPham.DonGia;
            _context.SaveChanges();
            return RedirectToAction("Cart", "Home");
        }
        public IActionResult XoaSPGioHang(int id)
        {
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            GioHang giohang = _context.GioHangs.Where(gio=>gio.SanPhamId == id && gio.TaiKhoanId == taikhoanid).FirstOrDefault();
            _context.GioHangs.Remove(giohang);
            _context.SaveChanges();
            return RedirectToAction("Cart", "Home");
        }
        public IActionResult Pay()
        {
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            List<GioHang> giohang = _context.GioHangs.Include(gio => gio.SanPham).Where(gio => gio.TaiKhoanId == taikhoanid).ToList();
            TaiKhoan taikhoan = _context.TaiKhoans.Find(taikhoanid);
            ViewBag.taikhoan = taikhoan;
            float tongtien = 0;
            foreach (var gio in giohang)
            {
                tongtien += gio.TongTien;
            }
            ViewBag.tongtien = tongtien;
            ViewBag.slsp = giohang.Count();
            return View(giohang);
        }
        public IActionResult TaoHoaDon(string HoTen, string SDT,string DChi,string thanhtoan)
        {
            bool tt = false;
            if(thanhtoan == "1")
            {
                tt = true;
            }
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            List<GioHang> giohang = _context.GioHangs.Include(gio => gio.SanPham).Where(gio => gio.TaiKhoanId == taikhoanid).ToList();
            float tongtien = 0;
            foreach (var gio in giohang)
            {
                tongtien += gio.TongTien;
            }
            Random rd = new Random();
            string mahd = taikhoanid.ToString()+ "_" + DateTime.Now.ToShortDateString()+ "_"+ rd.Next(0,10000).ToString();
            HoaDon hoaDon = new HoaDon();
            hoaDon.MAHD = mahd;
            hoaDon.TaiKhoanId = taikhoanid;
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.ThanhToan = tt;
            hoaDon.DChiGiaoHang = DChi;
            hoaDon.SDTGiaoHang = SDT;
            hoaDon.TenNguoiNhan = HoTen;
            hoaDon.TongTien = tongtien;
            hoaDon.TrangThai = false;
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();
            int idhoadon = _context.HoaDons.Where(hd => hd.MAHD == mahd).Select(hd => hd.HoaDonId).FirstOrDefault();
            foreach (var gio in giohang)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.HoaDonId = idhoadon;
                cthd.SanPhamId = gio.SanPhamId;
                cthd.SL = gio.SL;
                cthd.DonGia = _context.SanPhams.Where(sp => sp.SanPhamId == gio.SanPhamId).Select(sp => sp.DonGia).FirstOrDefault();
                _context.ChiTietHoaDons.Add(cthd);
                _context.GioHangs.Remove(gio);
                _context.SaveChanges();
            }
            return RedirectToAction("index", "Home");
        }
        public IActionResult Search(string thongtin)
        {
            var lstsanpham = _context.SanPhams.Include(sp=>sp.LoaiSP).Where(sp=>sp.MASP.Contains(thongtin) || sp.TenSP.Contains(thongtin));
            return View(lstsanpham);
        }
    }
}
