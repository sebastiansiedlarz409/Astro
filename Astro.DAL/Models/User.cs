using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Astro.DAL.Models
{
    public class User : IdentityUser
    {
        public int TopicsCount { get; set; }

        public int CommentsCount { get; set; }

        public string LastLoginDate { get; set; }

        public string RegisterDate { get; set; }

        public List<Topic> Topics { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
