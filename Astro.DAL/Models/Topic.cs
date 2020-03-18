using System.Collections.Generic;

namespace Astro.DAL.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public User User { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
