using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cycle.Data;
using cycle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cycle.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestomoniaController : Controller
    {
        private readonly ILogger<TestomoniaController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestomoniaController(ILogger<TestomoniaController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var testomonia = _context.Testomonias.OrderByDescending(x => x.Id).ToList();
            return View(testomonia);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string content, string name, IFormFile photo)
        {

            var path = "/uploads/" + Guid.NewGuid() + photo.FileName;
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            Testomonia testomonia = new Testomonia();
            testomonia.content = content;
            testomonia.name = name;
            testomonia.PhotoUrl =path;
            _context.Testomonias.Add(testomonia);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}