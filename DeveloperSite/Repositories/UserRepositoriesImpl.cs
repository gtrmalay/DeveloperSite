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
            if (user.User_id == 0) 
            {
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Update(user);
            }

            _context.SaveChanges();
        }

        public  User GetUserByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(x => x.User_email == email);

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Пользователь не найден: {ex.Message}");
                return null;
            }
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
