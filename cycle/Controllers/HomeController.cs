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
            var prodacts = _context.Prodacts.OrderByDescending(x=>x.Id).ToList();
            var banner = _context.Banners.ToList();
            var about = _context.Abouts.OrderByDescending(x=>x.Id).First();
            HomeVM homeVM = new HomeVM()
            {
                Banners = banner,
                Prodacts =prodacts,
                About = about
            };
            return View(homeVM);
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