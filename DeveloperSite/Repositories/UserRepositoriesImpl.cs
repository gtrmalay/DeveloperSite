using DeveloperSite.Models;
using DeveloperSite.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperSite.Repositories
{
    public class UserRepositoriesImpl : UserRepositories
    {
        private MobileContext _context;
        public UserRepositoriesImpl(MobileContext context)
        {
            this._context = context;
        }
        public void SaveUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        User UserRepositories.GetUserByEmail(string email)
        {
            User User = new User();
            try
            {
                User = _context.Users.FirstOrDefault(x => x.User_email == email);

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Пользователь не найден: {ex.Message}");
            }
            return User;
        }
    }
}
