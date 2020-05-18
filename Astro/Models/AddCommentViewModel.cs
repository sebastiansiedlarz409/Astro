using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Astro.Models
{
    public class AddCommentViewModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Comment { get; set; }
    }
}
