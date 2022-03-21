using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                SanPhamBanChay("thang");
                int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
                TaiKhoan taikhoan = _context.TaiKhoans.Where(tk => tk.TaiKhoanId == taikhoanid && tk.TrangThai == true).FirstOrDefault();
                if (taikhoan.LoaiTKId == 1)
                {
                    ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
                    ViewBag.slnguoidung = _context.TaiKhoans.Where(tk => tk.LoaiTKId == 2).Count();
                    ViewBag.slgiohang = _context.GioHangs.GroupBy(cart => cart.TaiKhoanId).Count();
                    DateTime date = DateTime.Today;
                    var dayofweek = DayOfWeek.Sunday;
                    var s = date.DayOfWeek;
                    if (s != dayofweek)
                    {
                        var a = dayofweek - s;
                        date = date.AddDays(a);
                    }
                    var hoadon = _context.HoaDons.Include(i => i.TaiKhoan).Where(i => i.NgayLap.Date >= date.Date);
                    var tatcahoadon = _context.HoaDons.Include(i => i.TaiKhoan);
                    ViewBag.hoadon = hoadon;
                    ViewBag.slhoadon = tatcahoadon.Count();
                    ViewBag.slsanpham = _context.SanPhams.Count();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return RedirectToAction("Login", "Home");
        }
       
        public IActionResult ThongKeSanPham(string thoigian)
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            SanPhamBanChay(thoigian);
            ViewBag.thongke = thoigian;
            return View();
        }
        public IActionResult ThongKeChiTiet()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            DateTime date = DateTime.Today;
            var dayofweek = DayOfWeek.Sunday;
            var s = date.DayOfWeek;
            if (s != dayofweek)
            {
                var a = dayofweek - s;
                date = date.AddDays(a);
            }
            var hoadonmoi = _context.HoaDons.Include(i => i.TaiKhoan).Where(i => i.NgayLap.Date >= date.Date);
            ViewBag.hoadonmoi = hoadonmoi;
            ViewBag.hoadon = new List<HoaDon>();
            var tiendachi = _context.HoaDons.Include(i => i.TaiKhoan).ToList();
            return View(tiendachi);
        }
        [HttpPost]
        public IActionResult ThongKeChiTiet(DateTime datebefore, DateTime dateafter,string tim)
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            if (tim == "doanhthu")
            {
                ViewBag.hoadon = new List<HoaDon>();
                ViewBag.doanhthu = _context.HoaDons.Where(i => i.NgayLap.Date >= datebefore.Date && i.NgayLap.Date <= dateafter.Date).Sum(i => i.TongTien).ToString("#,##0");
                if(datebefore == dateafter)
                {
                    ViewBag.khoangthoigiandoanhthu = " trong ngày " + datebefore.Date.ToShortDateString();
                }
                else
                {
                    ViewBag.khoangthoigiandoanhthu = " từ ngày " + datebefore.Date.ToShortDateString() + " đến ngày " + dateafter.Date.ToShortDateString();
                }
                
            }
            else
            {
                ViewBag.hoadon = _context.HoaDons.Where(i => i.NgayLap.Date >= datebefore.Date && i.NgayLap.Date <= dateafter.Date).ToList();
                if (datebefore == dateafter)
                {
                    ViewBag.khoangthoigianhoadon = " trong ngày " + datebefore.Date.ToShortDateString();
                }
                else
                {
                    ViewBag.khoangthoigianhoadon = " từ ngày " + datebefore.Date.ToShortDateString() + " đến ngày " + dateafter.Date.ToShortDateString();
                }
            }
            DateTime date = DateTime.Today;
            var dayofweek = DayOfWeek.Sunday;
            var s = date.DayOfWeek;
            if (s != dayofweek)
            {
                var a = dayofweek - s;
                date = date.AddDays(a);
            }
            var hoadonmoi = _context.HoaDons.Include(i => i.TaiKhoan).Where(i => i.NgayLap.Date >= date.Date).ToList();
            ViewBag.hoadonmoi = hoadonmoi;

            var tiendachi = _context.HoaDons.Include(i => i.TaiKhoan).ToList();
            return View(tiendachi);
        }
        void SanPhamBanChay(string thoigian)
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var date = DateTime.Now.AddDays((-DateTime.Now.Day) + 1).Date.AddHours(-11).AddMinutes(-59);
            if (thoigian == "quy")
            {
                var t2 = date.AddDays((-date.Day) + 1).Date.AddHours(-11).AddMinutes(-59);
                var t3 = t2.AddDays((-t2.Day) + 1).Date.AddHours(-11).AddMinutes(-59);
                date = t3;
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                if (date.Date < firstDay)
                {
                    date = firstDay.AddHours(-11).AddMinutes(-59);
                }
            }
            if (thoigian == "nam")
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                date = firstDay.AddHours(-11).AddMinutes(-59);
            }
            var sanphambanchays = _context.ChiTietHoaDons.Include(cthd => cthd.SanPham).Where(cthd => cthd.HoaDon.NgayLap.Date > date).ToList().GroupBy(cthd => cthd.SanPhamId).SelectMany(cl => cl.Select(
               csLine => new SanPham
               {
                   SanPhamId = csLine.SanPham.SanPhamId,
                   MASP = csLine.SanPham.MASP,
                   TenSP = csLine.SanPham.TenSP,
                   DonGia = csLine.SanPham.DonGia,
                   Anh = csLine.SanPham.Anh.ToString(),
                   SL = cl.Sum(c => c.SL),
               })).OrderByDescending(i => i.SL).Distinct().ToList();

            int i = 1;
            while (i < sanphambanchays.Count)
            {
                if (i == 1)
                {
                    if (sanphambanchays[i].SanPhamId == sanphambanchays[0].SanPhamId)
                    {
                        sanphambanchays.Remove(sanphambanchays[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    if (sanphambanchays[i].SanPhamId == sanphambanchays[i - 1].SanPhamId)
                    {
                        sanphambanchays.Remove(sanphambanchays[i]);
                    }
                    else
                    {
                        i++;
                    }
                }

            }
            ViewBag.sanphambanchay = sanphambanchays;
        }
       
    }
    
}
