using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace DeveloperSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MobileContext _context;
        private readonly UserRepositoriesImpl _userRepository;
        private readonly GameRepositories _gameRepository;

        public HomeController(MobileContext context, ILogger<HomeController> logger, UserRepositoriesImpl userRepository, GameRepositories gameRepositories)
        {
            _context = context;
            _logger = logger;
            _userRepository = userRepository;
            _gameRepository = gameRepositories;
        }


        public IActionResult Index()
        {
            return View();
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

        private int _counter = 0;

        public IActionResult PersonalArea()
        {
            try
            {
                if (int.TryParse(HttpContext.Session.GetString("isAutorize"), out int isAuthorized) && isAuthorized == 1)
                {
                    ViewBag.UserName = HttpContext.Session.GetString("UserName");
                    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
                    return View("~/Views/Registration/Profile.cshtml");
                }
                else
                {
                    return View("~/Views/Home/PersonalArea.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Что-то пошло не так!");
                ViewBag.ErrorMessage = "Что-то пошло не так. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }

       
    }
}
