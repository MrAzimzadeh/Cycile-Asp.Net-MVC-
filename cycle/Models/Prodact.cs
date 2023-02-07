using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cycle.Models
{
    public class Prodact
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }

        public string PhotoUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
