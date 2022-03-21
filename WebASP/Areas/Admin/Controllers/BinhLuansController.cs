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
    public class BinhLuansController : Controller
    {
        private readonly WebASPContext _context;

        public BinhLuansController(WebASPContext context)
        {
            _context = context;
        }

        // GET: Admin/BinhLuans
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.BinhLuans.Include(b => b.SanPham).Include(b => b.TaiKhoan);
            return View(await webASPContext.ToListAsync());
        }
        public IActionResult SuaTrangThai(int id)
        {
            var binhLuan = _context.BinhLuans.Find(id);
            if (binhLuan.trangthai == true)
            {
                binhLuan.trangthai = false;
            }
            else if (binhLuan.trangthai == false)
            {
                binhLuan.trangthai = true;
            }
            _context.BinhLuans.Update(binhLuan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        // GET: Admin/BinhLuans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuans
                .Include(b => b.SanPham)
                .Include(b => b.TaiKhoan)
                .FirstOrDefaultAsync(m => m.BinhLuanId == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Create
        public IActionResult Create()
        {
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId");
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "TaiKhoanId", "TaiKhoanId");
            return View();
        }

        // POST: Admin/BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BinhLuanId,noidung,trangthai,thoigian,TaiKhoanId,SanPhamId")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", binhLuan.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "TaiKhoanId", "TaiKhoanId", binhLuan.TaiKhoanId);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuans.FindAsync(id);
            if (binhLuan == null)
            {
                return NotFound();
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", binhLuan.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "TaiKhoanId", "TaiKhoanId", binhLuan.TaiKhoanId);
            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BinhLuanId,noidung,trangthai,thoigian,TaiKhoanId,SanPhamId")] BinhLuan binhLuan)
        {
            if (id != binhLuan.BinhLuanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(binhLuan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinhLuanExists(binhLuan.BinhLuanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", binhLuan.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoans, "TaiKhoanId", "TaiKhoanId", binhLuan.TaiKhoanId);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var binhLuan = await _context.BinhLuans
                .Include(b => b.SanPham)
                .Include(b => b.TaiKhoan)
                .FirstOrDefaultAsync(m => m.BinhLuanId == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var binhLuan = await _context.BinhLuans.FindAsync(id);
            _context.BinhLuans.Remove(binhLuan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BinhLuanExists(int id)
        {
            return _context.BinhLuans.Any(e => e.BinhLuanId == id);
        }
    }
}
