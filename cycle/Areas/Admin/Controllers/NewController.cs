using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cycle.Data;
using cycle.Helpers;
using cycle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cycle.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NewController> _logger;
        private readonly IWebHostEnvironment _env;

        public NewController(ILogger<NewController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var news = _context.News.OrderByDescending(x => x.Id).ToList();
            return View(news);
        }
        
        public IActionResult Error()
        {
            
            return View();
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(string title, string userName, string content, IFormFile photo)
        {
            News news = new News();
            news.PhotoUrl = ImageHelper.UploadSinglePhoto(photo, _env);
            news.UserName = userName;
            news.Content = content;
            news.PublishDate = DateTime.Now;
            news.Title = title;
            _context.News.Add(news);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int Id)
        {
            var detail = _context.News.FirstOrDefault(x=>x.Id == Id);
            if (detail == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(detail);
        }
    }
}