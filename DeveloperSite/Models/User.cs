namespace DeveloperSite.Models
{
    public class User
    {
        public long User_id { get; set; }
        public string User_name { get; set; }
        public string User_email { get; set; }
        public string User_password { get; set; }

        public User() { }

        public User(string user_name, string user_email, string user_password)
        {
            User_name = user_name;
            User_email = user_email;
            User_password = user_password;
        }
    }
}
