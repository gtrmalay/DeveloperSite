using DeveloperSite.Models;
using DeveloperSite.Models;

namespace DeveloperSite.Repositories
{
    public interface UserRepositories
    {
        User GetUserByEmail(string email);
        void SaveUser(User user);

        public void DeleteUser(User user);
    }
}
