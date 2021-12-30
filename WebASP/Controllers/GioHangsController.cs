using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Controllers
{
    public class GioHangsController : Controller
    {
        private readonly WebASPContext _context;

        public GioHangsController(WebASPContext context)
        {
            _context = context;
        }

        // GET: GioHangs
        public async Task<IActionResult> Index()
        {
            var webASPContext = _context.GioHangs.Include(g => g.SanPham).Include(g => g.TaiKhoan);
            return View(await webASPContext.ToListAsync());
        }

        // GET: GioHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs
                .Include(g => g.SanPham)
                .Include(g => g.TaiKhoan)
                .FirstOrDefaultAsync(m => m.GioHangId == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // GET: GioHangs/Create
        public IActionResult Create()
        {
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId");
            ViewData["TaiKhoanId"] = new SelectList(_context.Set<TaiKhoan>(), "TaiKhoanId", "TaiKhoanId");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GioHangId,TaiKhoanId,SanPhamId,SL,DonGia,TongTien")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", gioHang.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.Set<TaiKhoan>(), "TaiKhoanId", "TaiKhoanId", gioHang.TaiKhoanId);
            return View(gioHang);
        }

        // GET: GioHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs.FindAsync(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", gioHang.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.Set<TaiKhoan>(), "TaiKhoanId", "TaiKhoanId", gioHang.TaiKhoanId);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GioHangId,TaiKhoanId,SanPhamId,SL,DonGia,TongTien")] GioHang gioHang)
        {
            if (id != gioHang.GioHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangExists(gioHang.GioHangId))
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
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "SanPhamId", gioHang.SanPhamId);
            ViewData["TaiKhoanId"] = new SelectList(_context.Set<TaiKhoan>(), "TaiKhoanId", "TaiKhoanId", gioHang.TaiKhoanId);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHangs
                .Include(g => g.SanPham)
                .Include(g => g.TaiKhoan)
                .FirstOrDefaultAsync(m => m.GioHangId == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioHang = await _context.GioHangs.FindAsync(id);
            _context.GioHangs.Remove(gioHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangExists(int id)
        {
            return _context.GioHangs.Any(e => e.GioHangId == id);
        }
    }
}
