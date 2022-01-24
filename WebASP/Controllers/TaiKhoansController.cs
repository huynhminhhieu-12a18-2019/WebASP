using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebASP.Data;
using WebASP.Models;

namespace WebASP.Controllers
{
    public class TaiKhoansController : Controller
    {
        private readonly WebASPContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TaiKhoansController(WebASPContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: TaiKhoans
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.TaiKhoans.Include(t => t.LoaiTK);
            return View(await webASPContext.ToListAsync());
        }

        // GET: TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.LoaiTK)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: TaiKhoans/Create
        public IActionResult Create()
        {
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai");
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,Email,Anh,AnhFile,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai", taiKhoan.LoaiTKId);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai", taiKhoan.LoaiTKId);
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,Email,Anh,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanId))
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
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai", taiKhoan.LoaiTKId);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.LoaiTK)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.TaiKhoanId == id);
        }
    }
}
