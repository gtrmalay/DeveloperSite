using DeveloperSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeveloperSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Registration()
        {
            return View("~/Views/Registration/Registration.cshtml");
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

        public IActionResult GamesList()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            return View();
        }

        public IActionResult PersonalArea()
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


        public IActionResult GetReg()
        {
            return View("~/Views/Registration/Registration.cshtml");
        }

        public IActionResult GetLog()
        {
            return View("~/Views/Registration/Login.cshtml");
        }

        public IActionResult AboutDeveloper()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            return View();
        }


    }
}