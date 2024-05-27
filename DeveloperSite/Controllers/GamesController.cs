using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DeveloperSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> _logger;
        private readonly MobileContext _context;
        private readonly GameRepositories _gameRepository;

        public GamesController(MobileContext context, ILogger<GamesController> logger, GameRepositories gameRepositories)
        {
            _context = context;
            _logger = logger;
            _gameRepository = gameRepositories;
        }

        public IActionResult ToGamePage(int id)
        {
            try
            {
                var game = _gameRepository.GetGameById(id);
                return View("~/Views/Home/GamePage.cshtml", game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Что-то пошло не так!");
                ViewBag.ErrorMessage = "Что-то пошло не так. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }

        public IActionResult GamesList()
        {
            try
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
                return View(_gameRepository.GetAllGames());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка вывода списка игр.");
                ViewBag.ErrorMessage = "Ошибка при выводе списка игр. Попробуйте позже.";
                return View("~/Views/Error/Error.cshtml");
            }
        }
    }
}
