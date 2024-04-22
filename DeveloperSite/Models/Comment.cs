namespace DeveloperSite.Models
{
    public class Comment
    {
        public long Comment_id { get; set; }
        public long Game_id { get; set; }
        public long User_id { get; set; }
        public string Comment_Text { get; set;}
        public string Comment_Title { get; set;}
    }
}
