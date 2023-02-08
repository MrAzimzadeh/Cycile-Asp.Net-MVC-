using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using cycle.Data;
using cycle.VievModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cycle.Areas.Admin.Controllers
{
  [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly AppDbContext _context;

        public CommentController(ILogger<CommentController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           var comments =  _context.Comments.OrderByDescending(x=>x.Id).ToList();
           MessageVM messageVm = new MessageVM()
           {
               Comments = comments
           };
            return View(messageVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}