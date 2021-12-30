using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebASP.Models;

namespace WebASP.Data
{
    public class WebASPContext : DbContext
    {
        public WebASPContext (DbContextOptions<WebASPContext> options)
            : base(options)
        {
        }

        public DbSet<WebASP.Models.ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public DbSet<WebASP.Models.SanPham> SanPhams { get; set; }

        public DbSet<WebASP.Models.GioHang> GioHangs { get; set; }

        public DbSet<WebASP.Models.HoaDon> HoaDons { get; set; }

        public DbSet<WebASP.Models.LoaiSP> LoaiSPs { get; set; }

        public DbSet<WebASP.Models.LoaiTK> LoaiTKs { get; set; }

        public DbSet<WebASP.Models.TaiKhoan> TaiKhoans { get; set; }
    }
}
