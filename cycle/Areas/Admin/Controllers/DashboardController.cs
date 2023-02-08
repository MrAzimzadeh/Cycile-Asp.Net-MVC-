using cycle.Data;
using cycle.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cycle.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DashboardController(ILogger<AboutController> logger, AppDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var comments = _context.Comments.OrderByDescending(x => x.Id).ToList();
            MessageVM messageVm = new MessageVM()
            {
                Comments = comments
            };
            return View(messageVm);
        }
    }
}
