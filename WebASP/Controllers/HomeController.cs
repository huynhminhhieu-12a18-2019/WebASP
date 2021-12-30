using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
            int loaiTKID = (from taikhoan in _context.TaiKhoans
                           where taikhoan.Ten == tk && taikhoan.MK == mk
                           select taikhoan.LoaiTKId).FirstOrDefault();
            if (loaiTKID!= 0)
            {
                if (loaiTKID == 1)
                {
                    return RedirectToAction("Index", "Admins");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
        }
        
        public IActionResult SingleProduct()
        {
            return View();
        }
    }
}
