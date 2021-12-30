using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class LoaiSP
    {
        public int LoaiSPId { get; set; }
        [DisplayName("Tên loại")]
        public string TenLoai { get; set; }

        public List<SanPham> SanPhams { get; set; }
    }
}
