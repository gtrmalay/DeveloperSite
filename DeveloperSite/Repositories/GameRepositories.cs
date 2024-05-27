using DeveloperSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperSite.Repositories
{
    public class GameRepositories : GameRepositoriesInterface
    {
        private readonly MobileContext _context;

        public GameRepositories(MobileContext context)
        {
            _context = context;
        }

        public Game GetGameById(int id)
        {
            return _context.Games.FirstOrDefault(g => g.Game_id == id);
        }

        public List<Game> GetAllGames()
        {
            return _context.Games.ToList();
        }
    }
}
