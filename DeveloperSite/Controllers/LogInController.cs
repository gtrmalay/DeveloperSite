using DeveloperSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DeveloperSite.Controllers
{
    public class LogInController : Controller
    {
        private readonly MobileContext _db;

        public LogInController(MobileContext context)
        {
            _db = context;
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.User_email == user.User_email);
            if (existingUser != null)
            {
                // Проверяем хэш введённого пароля с хэшем сохранённого пароля
                if (HashHelper.HashString(user.User_password) == existingUser.User_password)
                {
                    HttpContext.Session.SetString("UserName", existingUser.User_name);
                    HttpContext.Session.SetString("UserEmail", existingUser.User_email);
                    ViewBag.UserName = HttpContext.Session.GetString("UserName");
                    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
                    RegistrationController.isAutorize = "1";
                    HttpContext.Session.SetString("isAutorize", RegistrationController.isAutorize);

                    return View("~/Views/Registration/Profile.cshtml");
                }
            }

            ModelState.AddModelError("LoginFailed", "Неверный email или пароль.");
            RegistrationController.isAutorize = "0";
            HttpContext.Session.SetString("isAutorize", RegistrationController.isAutorize);
            return View("~/Views/Registration/LoginError.cshtml", user);
        }
    }
}
