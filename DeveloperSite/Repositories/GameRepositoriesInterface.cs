using DeveloperSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperSite.Repositories
{
    public interface GameRepositoriesInterface
    {
        Game GetGameById(int id);
        List<Game> GetAllGames();
    }
}
