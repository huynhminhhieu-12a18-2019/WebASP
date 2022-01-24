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
    public class SanPhamsController : Controller
    {
        private readonly WebASPContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SanPhamsController(WebASPContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var webASPContext = _context.SanPhams.Include(s => s.LoaiSP);
            return View(await webASPContext.ToListAsync());
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            ViewData["LoaiSPId"] = new SelectList(_context.LoaiSPs, "LoaiSPId", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanPhamId,MASP,TenSP,LoaiSPId,DonGia,Anh,AnhFile,SL,MoTa,Trangthai")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                if (sanPham.AnhFile != null)
                {
                    var filename = sanPham.SanPhamId.ToString() + Path.GetExtension(sanPham.AnhFile.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "sanpham");
                    var filePath = Path.Combine(uploadPath, filename);
                    using(FileStream fs = System.IO.File.Create(filePath))
                    {
                        sanPham.AnhFile.CopyTo(fs);
                        fs.Flush();
                    }
                    sanPham.Anh = filename;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiSPId"] = new SelectList(_context.LoaiSPs, "LoaiSPId", "LoaiSPId", sanPham.LoaiSPId);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["LoaiSPId"] = new SelectList(_context.LoaiSPs, "LoaiSPId", "LoaiSPId", sanPham.LoaiSPId);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SanPhamId,MASP,TenSP,LoaiSPId,DonGia,Anh,AnhFile,SL,MoTa,Trangthai")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sanPham.Anh = (from sp in _context.SanPhams
                                   where sp.SanPhamId == sanPham.SanPhamId
                                   select sp.Anh).FirstOrDefault();
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    if (sanPham.AnhFile != null)
                    {
                        var filename = sanPham.SanPhamId.ToString() + Path.GetExtension(sanPham.AnhFile.FileName);
                        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "sanpham");
                        var filePath = Path.Combine(uploadPath, filename);
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            sanPham.AnhFile.CopyTo(fs);
                            fs.Flush();
                        }
                        sanPham.Anh = filename;
                        _context.Update(sanPham);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.SanPhamId))
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
            ViewData["LoaiSPId"] = new SelectList(_context.LoaiSPs, "LoaiSPId", "LoaiSPId", sanPham.LoaiSPId);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.LoaiSP)
                .FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.SanPhamId == id);
        }
    }
}
