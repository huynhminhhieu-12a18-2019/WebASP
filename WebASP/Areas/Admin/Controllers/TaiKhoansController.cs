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

namespace WebASP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoansController : Controller
    {
        private readonly WebASPContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TaiKhoansController(WebASPContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/TaiKhoans
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var taikhoanid = Convert.ToInt32(HttpContext.Request.Cookies["TaiKhoanId"].ToString());
            var webASPContext = _context.TaiKhoans.Include(t => t.LoaiTK).Where(t => t.TaiKhoanId != taikhoanid && t.TaiKhoanId != 1);
            return View(await webASPContext.ToListAsync());
        }
        public IActionResult SuaTrangThai(int id)
        {
            var taikhoan = _context.TaiKhoans.Find(id);
            if (taikhoan.TrangThai == true)
            {
                taikhoan.TrangThai = false;
            }
            else if (taikhoan.TrangThai == false)
            {
                taikhoan.TrangThai = true;
            }
            _context.TaiKhoans.Update(taikhoan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        // GET: Admin/TaiKhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.LoaiTK)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
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
        // GET: Admin/TaiKhoans/Create
        public IActionResult Create()
        {
            tinhNgayThangNam();
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,ThSinh,NamSinh,Email,Anh,AnhFile,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
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

        // GET: Admin/TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            tinhNgayThangNam();
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["LoaiTKId"] = new SelectList(_context.LoaiTKs, "LoaiTKId", "TenLoai", taiKhoan.LoaiTKId);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaiKhoanId,Ten,MK,HoTen,DChi,NgSinh,ThSinh,NamSinh,Email,Anh,AnhFile,SDT,LoaiTKId,TrangThai")] TaiKhoan taiKhoan)
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

        // GET: Admin/TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.LoaiTK)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
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
