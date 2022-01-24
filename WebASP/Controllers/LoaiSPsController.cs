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
    public class LoaiSPsController : Controller
    {
        private readonly WebASPContext _context;

        public LoaiSPsController(WebASPContext context)
        {
            _context = context;
        }

        // GET: LoaiSPs
        public async Task<IActionResult> Index()
        {
            ViewBag.TaiKhoan = HttpContext.Request.Cookies["HoTen"].ToString();
            return View(await _context.LoaiSPs.ToListAsync());
        }

        // GET: LoaiSPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs
                .FirstOrDefaultAsync(m => m.LoaiSPId == id);
            if (loaiSP == null)
            {
                return NotFound();
            }

            return View(loaiSP);
        }

        // GET: LoaiSPs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiSPId,TenLoai")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSP);
        }

        // GET: LoaiSPs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            if (loaiSP == null)
            {
                return NotFound();
            }
            return View(loaiSP);
        }

        // POST: LoaiSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiSPId,TenLoai")] LoaiSP loaiSP)
        {
            if (id != loaiSP.LoaiSPId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSPExists(loaiSP.LoaiSPId))
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
            return View(loaiSP);
        }

        // GET: LoaiSPs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSP = await _context.LoaiSPs
                .FirstOrDefaultAsync(m => m.LoaiSPId == id);
            if (loaiSP == null)
            {
                return NotFound();
            }

            return View(loaiSP);
        }

        // POST: LoaiSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSP = await _context.LoaiSPs.FindAsync(id);
            _context.LoaiSPs.Remove(loaiSP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSPExists(int id)
        {
            return _context.LoaiSPs.Any(e => e.LoaiSPId == id);
        }
    }
}
