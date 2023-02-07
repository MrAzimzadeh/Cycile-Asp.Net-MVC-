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
    public class ProdactController : Controller
    {
        private readonly ILogger<ProdactController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProdactController(ILogger<ProdactController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var prodact = _context.Prodacts.ToList();
            return View(prodact);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(string name , string content , Decimal price , IFormFile photo)
        {
            var path = "/uploads/" + Guid.NewGuid() + photo.FileName;
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            Prodact prodact = new Prodact();
            prodact.Name =name;
            prodact.Content = content;
            prodact.Price = price;
            prodact.PhotoUrl =path;
            _context.Prodacts.Add(prodact);
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