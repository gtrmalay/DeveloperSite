using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperSite.Controllers
{
    public class AboutDeveloperController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MobileContext _context;
        private readonly UserRepositoriesImpl _userRepository;
        public AboutDeveloperController(MobileContext context, ILogger<HomeController> logger, UserRepositoriesImpl userRepository)
        {
            _context = context;
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult AboutDeveloper()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            return View();
        }
    }
}
