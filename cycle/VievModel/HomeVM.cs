using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cycle.Models;

namespace cycle.VievModel
{
    public class HomeVM
    {
        public List<Banner> Banners { get; set; }
        public List<Prodact> Prodacts { get; set; }
        public About About { get; set; }
        public List<Testomonia> Testomonias { get; set; }
    }
}