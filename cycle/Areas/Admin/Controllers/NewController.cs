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
            var detail = _context.News.FirstOrDefault(x => x.Id == Id);
            if (detail == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(detail);
        }
        public IActionResult Delete(int Id)
        {
            var delete = _context.News.FirstOrDefault(x => x.Id == Id);
            return View(delete);
        }
        [HttpPost]
        public IActionResult Delete(News news)
        {
            var delete = _context.News.FirstOrDefault(x => x.Id == news.Id);
            _context.News.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int Id)
        {
            var update = _context.News.FirstOrDefault(x => x.Id == Id);
            if (update.Id == null)
            {
                return RedirectToAction(nameof(Error));
            }
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(News m, IFormFile photo)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == m.Id);
            if (news == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (photo != null)
            {
                news.PhotoUrl = ImageHelper.UploadSinglePhoto(photo, _env);
            }

            news.Title = m.Title;
            news.Content = m.Content;
            news.PublishDate = DateTime.Now;
            news.UserName = m.UserName;
            _context.News.Update(news);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}