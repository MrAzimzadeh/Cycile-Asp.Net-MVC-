using cycle.Data;
using cycle.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using cycle.VievModel;

namespace cycle.Areas.Admin.Controllers

{
    [Area("Admin")]

    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            var banners = _context.Banners.ToList();
            var comment = _context.Comments.OrderByDescending(x => x.Id).ToList();

            MessageVM messageVM = new MessageVM()
            {
                Banners = banners
                ,Comments = comment
            };

            return View(messageVM);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(string title , string Content , IFormFile Photo)
        {
            Banner banner = new Banner();
            banner.Title = title;
            banner.Content = Content;

            string uploadsFolder = Path.Combine(_env.WebRootPath, "Uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            Photo.CopyTo(fileStream);
            banner.PhotoUrl= uniqueFileName;

            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
