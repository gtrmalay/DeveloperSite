using Microsoft.AspNetCore.Mvc;

namespace DeveloperSite.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Registration/Login.cshtml");
        }
    }
}
