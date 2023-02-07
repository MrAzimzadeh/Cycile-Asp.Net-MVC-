using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cycle.Models
{
    public class About
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Basliq minumum 3 simvol olmalidir.")]
        public string Title { get; set; }
        [MinLength(3, ErrorMessage = "Basliq minumum 3 simvol olmalidir.")]
        public string content { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
    }
}