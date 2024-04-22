using DeveloperSite.Models;
using Microsoft.AspNetCore.Mvc;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Http;

namespace DeveloperSite.Controllers
{
    public class RegistrationController : Controller
    {
        MobileContext db;
        public static string isAutorize = "0";

        public RegistrationController(MobileContext context)
        {
            db = context;
        }

        public IActionResult GetReg()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            return View("~/Views/Registration/Registration.cshtml");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.User_password = HashHelper.HashString(user.User_password);

            UserRepositoriesImpl userRepository = new UserRepositoriesImpl(db);
            userRepository.SaveUser(user);

            HttpContext.Session.SetString("UserName", user.User_name);
            HttpContext.Session.SetString("UserEmail", user.User_email);
            HttpContext.Session.SetString("isAutorize", "1");

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");

            return View("~/Views/Registration/Profile.cshtml");
        }

        
    }
}
