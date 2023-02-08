using cycle.Data;
using cycle.Models;
using cycle.VievModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cycle.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var prodacts = _context.Prodacts.OrderByDescending(x => x.Id).ToList();
            var banner = _context.Banners.ToList();
            var about = _context.Abouts.OrderByDescending(x => x.Id).First();
            var testomonia = _context.Testomonias.OrderByDescending(x => x.Id).ToList();
            HomeVM homeVM = new HomeVM()
            {
                Banners = banner,
                Prodacts = prodacts,
                About = about,
                Testomonias = testomonia
            };
            return View(homeVM);
        }

        public IActionResult Comment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Comment(string userName, string userEmail , string userPhone , string userMessage)
        {
            Comment comment  = new Comment();
            comment.UserName = userName;
            comment.UserEmail = userEmail;
            comment.UserMessage = userMessage;
            comment.UserPhone = userPhone;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}