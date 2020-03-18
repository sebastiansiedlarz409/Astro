namespace Astro.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Content { get; set; }
        
        public Topic Topic { get; set; }
        public User User { get; set; }
    }
}
