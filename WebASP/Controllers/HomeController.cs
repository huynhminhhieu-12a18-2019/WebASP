using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebASPContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(WebASPContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
                ViewBag.TaiKhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            }
            ViewBag.banners = _context.Banners.ToList();
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s=>s.Trangthai == true).OrderByDescending(s=>s.SanPhamId).Take(4);
            var date = DateTime.Now.AddDays(-30).Date.AddHours(-11).AddMinutes(-59);
            var sanphambanchays = _context.ChiTietHoaDons.Include(cthd => cthd.SanPham).Where(cthd=>cthd.HoaDon.NgayLap.Date > date).ToList().GroupBy(cthd => cthd.SanPhamId).SelectMany(cl => cl.Select(
                    csLine => new SanPham
                    {
                        SanPhamId = csLine.SanPham.SanPhamId,
                        TenSP = csLine.SanPham.TenSP,
                        DonGia = csLine.SanPham.DonGia,
                        Anh = csLine.SanPham.Anh.ToString(),
                        SL = cl.Sum(c => c.SL),
                    })).OrderByDescending(i=>i.SL).Distinct().ToList();

            int i = 1;
            while (i < sanphambanchays.Count)
            {
                if(i == 1)
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
            return View(sanphams);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> capNhatTaiKhoan(int id, [Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,ThSinh,NamSinh,Email,Anh,AnhFile,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
        {

            if (ModelState.IsValid)
            {
                    taiKhoan.Anh = (from tk in _context.TaiKhoans
                                    where tk.TaiKhoanId == taiKhoan.TaiKhoanId
                                    select tk.Anh).FirstOrDefault();
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                    if (taiKhoan.AnhFile != null)
                    {
                        var filename = taiKhoan.TaiKhoanId.ToString() + Path.GetExtension(taiKhoan.AnhFile.FileName);
                        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "taikhoan");
                        var filePath = Path.Combine(uploadPath, filename);
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            taiKhoan.AnhFile.CopyTo(fs);
                            fs.Flush();
                        }
                        taiKhoan.Anh = filename;
                        _context.Update(taiKhoan);
                        await _context.SaveChangesAsync();
                    }
               
               
            }
            return RedirectToAction(nameof(MyAccount));
        }
        void tinhNgayThangNam()
        {
            List<int> ngay = new List<int>();
            List<int> thang = new List<int>();
            List<int> nam = new List<int>();
            for (int i = 1; i <= 31; i++)
            {
                if (i <= 12)
                {
                    thang.Add(i);
                }
                ngay.Add(i);
            }
            for (int i = 1970; i <= DateTime.Today.Year; i++)
            {
                nam.Add(i);
            }
            ViewBag.nam = nam;
            ViewBag.thang = thang;
            ViewBag.ngay = ngay;
        }
        public IActionResult MyAccount()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                return RedirectToAction("Login", "Home");
            }

            tinhNgayThangNam();
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            int id = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            var taiKhoan = _context.TaiKhoans.Where(tk => tk.TaiKhoanId == id).FirstOrDefault();
            return View(taiKhoan);
        }
        public IActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePass(string MKC, string MKM, string XNMKM)
        {
            if(MKM != XNMKM)
            {
                ViewBag.loi = "Nhập lại mật khẩu mới không khớp";
                return View();
            }
            else
            {
                int id = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
                var taiKhoan = _context.TaiKhoans.Where(tk => tk.TaiKhoanId == id).FirstOrDefault();
                if(taiKhoan.MK != MKC)
                {
                    ViewBag.loi = "Mật khẩu cũ nhập không chính xác";
                    return View();
                }
                else
                {
                    taiKhoan.MK = MKM;
                    _context.Update(taiKhoan);
                    _context.SaveChanges();
                    ViewBag.loi = "Đổi mật khẩu thành công";
                    return View();
                }
            }
            return View();
        }
        public IActionResult Register()
        {
            tinhNgayThangNam();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string XNMK,[Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,ThSinh,NamSinh,Email,Anh,AnhFile,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                if(XNMK != taiKhoan.MK)
                {
                    ViewBag.loi = "Mật khẩu không khớp";
                    return View();
                }
                taiKhoan.NgSinh = 1;
                taiKhoan.ThSinh = 1;
                taiKhoan.NamSinh = 1970;
                taiKhoan.LoaiTKId = 2;
                taiKhoan.TrangThai = true;
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                if (taiKhoan.AnhFile != null)
                {
                    var filename = taiKhoan.TaiKhoanId.ToString() + Path.GetExtension(taiKhoan.AnhFile.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "taikhoan");
                    var filePath = Path.Combine(uploadPath, filename);
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        taiKhoan.AnhFile.CopyTo(fs);
                        fs.Flush();
                    }
                    taiKhoan.Anh = filename;
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Login));
        }
        public IActionResult binhluan(int id,[Bind("BinhLuanId, noidung, trangthai, thoigian, TaiKhoanId, SanPhamId")] BinhLuan binhluan)
        {
            binhluan.thoigian = DateTime.Now;
            binhluan.TaiKhoanId = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            binhluan.trangthai = false;
            binhluan.SanPhamId = id;
            _context.Add(binhluan);
             _context.SaveChanges();
            CookieOptions cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            HttpContext.Response.Cookies.Append("SPID", id.ToString(), cookieOptions);
            return RedirectToAction(nameof(SingleProduct));
        }
        public IActionResult AllProducts()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s => s.Trangthai == true);
            return View(sanphams);
        }
        public IActionResult Asus()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s=> s.LoaiSP.TenLoai.Contains("ASUS") && s.Trangthai == true);
            return View(sanphams);
        }
        public IActionResult Acer()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s => s.LoaiSP.TenLoai.Contains("ACER") && s.Trangthai == true);
            return View(sanphams);
        }
        public IActionResult Dell()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s => s.LoaiSP.TenLoai.Contains("DELL") && s.Trangthai == true);
            return View(sanphams);
        }
        public IActionResult Msi()
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            var sanphams = _context.SanPhams.Include(s => s.LoaiSP).Where(s => s.LoaiSP.TenLoai.Contains("MSI") && s.Trangthai == true);
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
            TaiKhoan taikhoan = _context.TaiKhoans.Include(tkhoan=>tkhoan.LoaiTK).Where(tkhoan => tkhoan.Ten == tk && tkhoan.MK == mk && tkhoan.TrangThai == true).FirstOrDefault();
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
                        return RedirectToAction("Index", "Admin", "Home");
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
        public IActionResult SingleProduct(int id, bool load)
        {
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            }
            if(id == 0)
            {
                id = Convert.ToInt32(HttpContext.Request.Cookies["SPID"].ToString());
                HttpContext.Response.Cookies.Append("SPID", "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
            }
            var sanPham = _context.SanPhams.Include(sp => sp.LoaiSP).Where(sp => sp.SanPhamId == id).FirstOrDefault();
            List<SanPham> sanphams = _context.SanPhams.Where(sp => sp.LoaiSP.TenLoai == sanPham.LoaiSP.TenLoai).ToList();
            ViewBag.sanphamtuongtu = sanphams;
            List<BinhLuan> binhluan = _context.BinhLuans.Include(b => b.TaiKhoan).Where(b => b.trangthai == true && b.SanPhamId == id).Take(3).ToList();
            if (load == true)
            {
                 binhluan = _context.BinhLuans.Include(b => b.TaiKhoan).Where(b => b.trangthai == true && b.SanPhamId == id).ToList();
            }
            
            ViewBag.binhluan = binhluan;
            ViewBag.sanPham = sanPham;
            return View();
        }
        public IActionResult Cart()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (HttpContext.Request.Cookies.ContainsKey("thongbao"))
            {
                ViewBag.loi = HttpContext.Request.Cookies["thongbao"].ToString();
                HttpContext.Response.Cookies.Append("thongbao", "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
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
        public IActionResult Addcart(int id, int SL)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                return RedirectToAction("Login", "Home");
            }
            int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
            GioHang giohang = _context.GioHangs.Include(gio=>gio.SanPham).FirstOrDefault(gio => gio.TaiKhoanId == taikhoanid && gio.SanPhamId == id);
            float tongtien = _context.SanPhams.Where(sp => sp.SanPhamId == id).Select(sp => sp.DonGia).FirstOrDefault();
            if (giohang == null)
            {
                giohang = new GioHang();
                giohang.TaiKhoanId = taikhoanid;
                giohang.SanPhamId = id;
                giohang.SL = SL;
                giohang.TongTien = tongtien;
                _context.GioHangs.Add(giohang);
            }
            else
            {
                giohang.SL += SL;
                giohang.TongTien += SL * giohang.SanPham.DonGia;
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
            foreach (var sanpham in giohang)
            {
                if(sanpham.SanPham.SL - sanpham.SL < 0)
                {
                    string thongbao = "Sản phẩm " + sanpham.SanPham.TenSP + " chỉ còn " + sanpham.SanPham.SL + " cái ";
                    CookieOptions cookieOptions = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(1)
                    };
                    HttpContext.Response.Cookies.Append("thongbao", thongbao, cookieOptions);
                    return RedirectToAction("Cart", "Home");
                }
            }
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
            hoaDon.TrangThai = 0;
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
                SanPham sanPham = _context.SanPhams.Find(cthd.SanPhamId);
                sanPham.SL = sanPham.SL - cthd.SL;
                _context.Update(sanPham);
                _context.SaveChanges();
            }
            return RedirectToAction("index", "Home");
        }
        [HttpPost]
        public IActionResult Search(string thongtin, string loai , int? pricemin, int? pricemax, int? type)
        {
            
            if(thongtin != null)
            {
                HttpContext.Response.Cookies.Append("thongtin", "", new CookieOptions() { Expires = DateTime.Now.AddDays(-1) });
                CookieOptions cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpContext.Response.Cookies.Append("thongtin", thongtin, cookieOptions);
            }
            else
            {
                thongtin = HttpContext.Request.Cookies["thongtin"];
            }
            List<LoaiSP> loaisps = _context.LoaiSPs.ToList();
            loaisps.Insert(0, new LoaiSP { LoaiSPId = -1, TenLoai = "Tất cả" });
            ViewBag.loaisp = loaisps;
            List<SanPham> lstsanpham = _context.SanPhams.Include(sp => sp.LoaiSP).Where(sp => sp.MASP.Contains(thongtin) || sp.TenSP.Contains(thongtin) || sp.MoTa.Contains(thongtin) || sp.MASP.Contains(thongtin)).ToList();
            if (loai == "loc")
            {
                if (type != null)
                {
                    ViewBag.loai = type;
                }
                if (pricemin == null)
                {
                    pricemin = 0;
                }
                else
                {
                    ViewBag.pricemin = pricemin;
                }
                if (pricemax == null)
                {
                    pricemax = Int32.MaxValue;
                }
                else
                {
                    ViewBag.pricemax = pricemax;
                }
                if (type != -1)
                {
                    lstsanpham = lstsanpham.Where(prod => prod.LoaiSPId == type).ToList();
                }
                lstsanpham = lstsanpham.Where(prod => prod.DonGia >= pricemin).Where(prod => prod.DonGia <= pricemax).ToList();
            }
            ViewBag.slsp = lstsanpham.Count();
            return View(lstsanpham);
        }
    }
}
