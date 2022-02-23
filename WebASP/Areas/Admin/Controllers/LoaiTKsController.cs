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
    public class LoaiTKsController : Controller
    {
        private readonly WebASPContext _context;

        public LoaiTKsController(WebASPContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiTKs
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            return View(await _context.LoaiTKs.ToListAsync());
        }

        // GET: Admin/LoaiTKs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiTK = await _context.LoaiTKs
                .FirstOrDefaultAsync(m => m.LoaiTKId == id);
            if (loaiTK == null)
            {
                return NotFound();
            }

            return View(loaiTK);
        }

        // GET: Admin/LoaiTKs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiTKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiTKId,TenLoai")] LoaiTK loaiTK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiTK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiTK);
        }

        // GET: Admin/LoaiTKs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiTK = await _context.LoaiTKs.FindAsync(id);
            if (loaiTK == null)
            {
                return NotFound();
            }
            return View(loaiTK);
        }

        // POST: Admin/LoaiTKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiTKId,TenLoai")] LoaiTK loaiTK)
        {
            if (id != loaiTK.LoaiTKId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiTK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiTKExists(loaiTK.LoaiTKId))
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
            return View(loaiTK);
        }

        // GET: Admin/LoaiTKs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiTK = await _context.LoaiTKs
                .FirstOrDefaultAsync(m => m.LoaiTKId == id);
            if (loaiTK == null)
            {
                return NotFound();
            }

            return View(loaiTK);
        }

        // POST: Admin/LoaiTKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiTK = await _context.LoaiTKs.FindAsync(id);
            _context.LoaiTKs.Remove(loaiTK);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiTKExists(int id)
        {
            return _context.LoaiTKs.Any(e => e.LoaiTKId == id);
        }
    }
}
