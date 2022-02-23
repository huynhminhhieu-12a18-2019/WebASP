using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Areas.Admin.Controllers
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
            //if (HttpContext.Session.Keys.Contains("TaiKhoanId"))
            //{
            //    ViewBag.HoTen = HttpContext.Session.GetString("HoTen");
            //}
            if (HttpContext.Request.Cookies.ContainsKey("HoTen"))
            {
                int taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"]);
                TaiKhoan taikhoan = _context.TaiKhoans.Where(tk => tk.TaiKhoanId == taikhoanid).FirstOrDefault();
                if (taikhoan.LoaiTKId == 1)
                {
                    ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return RedirectToAction("Login", "Home");
        }
    }
    }
}
