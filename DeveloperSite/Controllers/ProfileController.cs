using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeveloperSite.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly MobileContext _context;
        private readonly UserRepositoriesImpl _userRepository;
        private readonly GameRepositories _gameRepository;

        public ProfileController(MobileContext context, ILogger<ProfileController> logger, UserRepositoriesImpl userRepository, GameRepositories gameRepositories)
        {
            _context = context;
            _logger = logger;
            _userRepository = userRepository;
            _gameRepository = gameRepositories;
        }

        [HttpPost]
        public IActionResult DeleteProfile(string confirmDelete)
        {
            try
            {
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = _userRepository.GetUserByEmail(userEmail);

                if (user != null && confirmDelete == userEmail)
                {
                    _userRepository.DeleteUser(user);
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorDelEmail = "Введенный email не совпадает с вашим.";
                    ViewBag.UserName = HttpContext.Session.GetString("UserName");
                    return View("~/Views/Registration/Profile.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении профиля для пользователя {UserEmail}", HttpContext.Session.GetString("UserEmail"));
                ViewBag.ErrorMessage = "Произошла ошибка при попытке удаления профиля. Попробуйте позже.";
                return View("~/Views/Registration/Profile.cshtml");
            }
        }

        [HttpPost]
        public IActionResult ExitProfile()
        {
            try
            {
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = _context.Users.FirstOrDefault(u => u.User_email == userEmail);

                if (user != null)
                {
                    HttpContext.Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("~/Views/Registration/Profile.cshtml");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выходе из профиля для пользователя {UserEmail}", HttpContext.Session.GetString("UserEmail"));
                ViewBag.ErrorMessage = "Произошла ошибка при попытке выхода из профиля. Попробуйте позже.";
                return View("~/Views/Registration/Profile.cshtml");
            }
        }

        [HttpPost]
        public IActionResult UpdateProfileName(string username)
        {
            try
            {
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = _userRepository.GetUserByEmail(userEmail);

                if (user != null)
                {
                    user.User_name = username;
                    _userRepository.SaveUser(user);
                    HttpContext.Session.SetString("UserName", username);
                }

                return RedirectToAction("PersonalArea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении имени профиля для пользователя {UserEmail}", HttpContext.Session.GetString("UserEmail"));
                ViewBag.ErrorMessage = "Произошла ошибка при обновлении имени профиля. Попробуйте позже.";
                return View("~/Views/Registration/Profile.cshtml");
            }
        }

        [HttpPost]
        public IActionResult UpdateProfilePass(string password)
        {
            try
            {
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = _userRepository.GetUserByEmail(userEmail);

                if (user != null)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        user.User_password = HashHelper.HashString(password);
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("PersonalArea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении пароля профиля для пользователя {UserEmail}", HttpContext.Session.GetString("UserEmail"));
                ViewBag.ErrorMessage = "Произошла ошибка при обновлении пароля профиля. Попробуйте позже.";
                return View("~/Views/Registration/Profile.cshtml");
            }
        }
    }
}
