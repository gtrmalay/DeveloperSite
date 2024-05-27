using DeveloperSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using DeveloperSite.Repositories;

namespace DeveloperSite.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserRepositoriesImpl _userRepository;
        private readonly ILogger<LogInController> _logger;
        public static bool isError = false;

        public LogInController(UserRepositoriesImpl userRepository, ILogger<LogInController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public IActionResult GetLog()
        {
            try
            {
                return View("~/Views/Registration/Login.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Что-то пошло не так!");
                ViewBag.ErrorMessage = "Что-то пошло не так. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var existingUser = _userRepository.GetUserByEmail(user.User_email);
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

                isError = true;
                return View("~/Views/Registration/LoginError.cshtml", user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при входе пользователя {UserEmail}", user.User_email);
                ViewBag.ErrorMessage = "Произошла ошибка при попытке входа. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }
    }
}
