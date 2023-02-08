using cycle.Data;
using cycle.Models;
using cycle.VievModel;
using Microsoft.AspNetCore.Mvc;

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
            var comment = _context.Comments.OrderByDescending(x => x.Id).ToList();

            var testomonia = _context.Testomonias.OrderByDescending(x => x.Id).ToList();
            MessageVM messageVm = new MessageVM()
            {
                Testomonias = testomonia
            ,Comments = comment
            };
            return View(messageVm);
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