using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cycle.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
    }
}