using System;
using System.Collections.Generic;
using System.Text;

namespace Astro.DAL.Models
{
    public class Insight
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }
        public string MaxTemp { get; set; }

        public string AvgTemp { get; set; }

        public string MinTemp { get; set; }

        public string MaxWind { get; set; }

        public string AvgWind { get; set; }
        
        public string MinWind { get; set; }

        public string MaxPress { get; set; }

        public string AvgPress { get; set; }

        public string MinPress { get; set; }

        public string Season { get; set; }
    }
}
