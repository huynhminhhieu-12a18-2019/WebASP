using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonsController : Controller
    {
        private readonly WebASPContext _context;

        public HoaDonsController(WebASPContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDons
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.HoaDons.Include(h => h.TaiKhoan);
            return View(await webASPContext.ToListAsync());
        }

        // GET: Admin/HoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var hoaDon = await _context.HoaDons
                .Include(h => h.TaiKhoan)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

       

        // GET: Admin/HoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var hoaDon = await _context.HoaDons
                .Include(h => h.TaiKhoan)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            _context.HoaDons.Remove(hoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.HoaDonId == id);
        }
        public IActionResult SuaTrangThai(int id, string trang)
        {
            var hoadon = _context.HoaDons.Find(id);
            if (hoadon.TrangThai == 0)
            {
                hoadon.TrangThai = 1;
            }
            else if(hoadon.TrangThai == 1)
            {
                hoadon.TrangThai = 2;
            }
            _context.HoaDons.Update(hoadon);
            _context.SaveChanges();
            if (trang == "index")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(HoaDonChuaDuyet));

        }
        public async Task<IActionResult> HoaDonChuaDuyet()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.HoaDons.Include(h => h.TaiKhoan).Where(hd => hd.TrangThai == 0);
            return View(await webASPContext.ToListAsync());
        }
        public async Task<IActionResult> HoaDonDaDuyet()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.HoaDons.Include(h => h.TaiKhoan).Where(hd => hd.TrangThai == 1);
            return View(await webASPContext.ToListAsync());
        }
        public async Task<IActionResult> HoaDonDaGiao()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.HoaDons.Include(h => h.TaiKhoan).Where(hd => hd.TrangThai == 2);
            return View(await webASPContext.ToListAsync());
        }
    }
}
