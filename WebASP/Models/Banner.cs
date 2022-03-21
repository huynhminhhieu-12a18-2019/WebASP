using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebASP.Models
{
    public class Banner
    {
        public int BannerId { get; set; }

        public string BannerString { get; set; }

        [DisplayName("Banner")]
        [NotMapped]
        public IFormFile BannerFile { get; set; }
    }
}
