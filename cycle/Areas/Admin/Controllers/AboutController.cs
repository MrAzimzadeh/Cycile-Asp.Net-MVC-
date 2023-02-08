using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cycle.Data;
using cycle.Models;
using cycle.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cycle.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(ILogger<AboutController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var about = _context.Abouts.OrderByDescending(x => x.Id).ToList();
            var comment = _context.Comments.OrderByDescending(x=>x.Id).ToList();    
            MessageVM messageVm = new MessageVM()
            {
                Abouts = about,
                Comments = comment
            };

            return View(messageVm);
        }

        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]

        public IActionResult Create(string title, string content, IFormFile photo)
        {
            var path = "/uploads/" + Guid.NewGuid() + photo.FileName;
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            About about = new About();
            about.content = content;
            about.PhotoUrl = path;
            about.Title = title;
            _context.Abouts.Add(about);
            _context.SaveChanges();
            Index();
            return RedirectToAction(nameof(Index));
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}