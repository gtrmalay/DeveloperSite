using Microsoft.EntityFrameworkCore;

namespace DeveloperSite.Models
{
    public class Game
    {
        public long Game_id { get; set; }
        public long Developer_id { get; set; }
        public string Game_name { get; set;}
        public string Game_description { get; set; }
        public string Genre { get; set;}
        public string Publicate_date { get; set; }
        public double Average_rating { get;set; }
        public string Link_for_download { get; set; }
    }
}
