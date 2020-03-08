using System;
using System.Collections.Generic;
using System.Text;

namespace Astro.DAL.Models
{
    public class APOD
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Url { get; set; }

        public string UrlHd { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string MediaType { get; set; }
    }
}
