using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class indexViewModel
    {
        public List<urunler> products { get; set; }
        public List<kategori> categories { get; set; }
        public List<urunler> categoryProducts { get; set; }
    }
}