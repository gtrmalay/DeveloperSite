using DeveloperSite.Models;
using DeveloperSite.Repositories;
using DeveloperSite.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeveloperSite.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly MobileContext _db;
        private readonly ILogger<RegistrationController> _logger;
        public static string isAutorize = "0";

        public RegistrationController(MobileContext context, ILogger<RegistrationController> logger)
        {
            _db = context;
            _logger = logger;
        }

        public IActionResult GetReg()
        {
            try
            {
                return View("~/Views/Registration/Registration.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Что-то пошло не так!");
                ViewBag.ErrorMessage = "Что-то пошло не так. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Register(SignUpDto signUp)
        {
            try
            {
                User user = new User(signUp.User_name, signUp.User_email, signUp.User_password)
                {
                    User_password = HashHelper.HashString(signUp.User_password)
                };

                UserRepositoriesImpl userRepository = new UserRepositoriesImpl(_db);
                userRepository.SaveUser(user);

                HttpContext.Session.SetString("UserName", user.User_name);
                HttpContext.Session.SetString("UserEmail", user.User_email);
                HttpContext.Session.SetString("isAutorize", "1");

                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");

                return View("~/Views/Registration/Profile.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации пользователя {UserEmail}", signUp.User_email);
                ViewBag.ErrorMessage = "Произошла ошибка при попытке. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }
    }
}
