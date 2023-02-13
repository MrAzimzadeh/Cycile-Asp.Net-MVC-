using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using cycle.Data;
using cycle.Helpers;
using cycle.Models;
using cycle.VievModel;
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
            var comment = _context.Comments.OrderByDescending(x => x.Id).ToList();

            MessageVM messageVm = new MessageVM()
            {
                Prodacts = prodact
                ,
                Comments = comment

            };
            return View(messageVm);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(string name, string content, Decimal price, IFormFile photo)
        {
            var path = "/uploads/" + Guid.NewGuid() + photo.FileName;
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            Prodact prodact = new Prodact();
            prodact.Name = name;
            prodact.Content = content;
            prodact.Price = price;
            prodact.PhotoUrl = path;
            _context.Prodacts.Add(prodact);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deteil(int Id)
        {
            var deteil = _context.Prodacts.FirstOrDefault(x => x.Id == Id);
            if (deteil == null)
            {
                return NotFound();
            }
            return View(deteil);
        }

        //??delete method
        public IActionResult Delete(int id)
        {
            var delete = _context.Prodacts.FirstOrDefault(x => x.Id == id);
            return View(delete);
        }

        [HttpPost]
        public IActionResult Delete(Prodact prodact)
        {
            var prodactDelete = _context.Prodacts.FirstOrDefault(x => x.Id == prodact.Id);
            if (prodactDelete == null)
            {
                return NotFound();
            }
            _context.Prodacts.Remove(prodactDelete);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            var update = _context.Prodacts.FirstOrDefault(x => x.Id == Id);
            if (update == null)
            {
                return NotFound();
            }
            return View(update);
        }

        [HttpPost]

        public IActionResult Update(Prodact prodact, IFormFile photo)
        {
            var existingProdact = _context.Prodacts.Find(prodact.Id);
            if (existingProdact == null)
            {
                return NotFound();
            }

            if (photo != null)
            {
                existingProdact.PhotoUrl = ImageHelper.UploadSinglePhoto(photo, _env);
            }

            existingProdact.Name = prodact.Name;
            existingProdact.Price = prodact.Price;
            existingProdact.Content = prodact.Content;

            _context.Prodacts.Update(existingProdact);
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