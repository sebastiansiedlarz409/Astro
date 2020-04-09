using System.ComponentModel.DataAnnotations.Schema;

namespace Astro.DAL.Models
{
    public class Insight
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }
        public string EndDate { get; set; }



        public string MaxTemp { get; set; }

        public string AvgTemp { get; set; }

        public string MinTemp { get; set; }

        [NotMapped]
        public string MaxTempC { get; set; }
        [NotMapped]
        public string AvgTempC { get; set; }
        [NotMapped]
        public string MinTempC { get; set; }
        [NotMapped]
        public string MaxTempK { get; set; }
        [NotMapped]
        public string AvgTempK { get; set; }
        [NotMapped]
        public string MinTempK { get; set; }



        public string MaxWind { get; set; }

        public string AvgWind { get; set; }
        
        public string MinWind { get; set; }
        [NotMapped]
        public string MaxWindKPH { get; set; }
        [NotMapped]
        public string AvgWindKPH { get; set; }
        [NotMapped]
        public string MinWindKPH { get; set; }
        [NotMapped]
        public string MaxWindMiPH { get; set; }
        [NotMapped]
        public string AvgWindMiPH { get; set; }
        [NotMapped]
        public string MinWindMiPH { get; set; }
        [NotMapped]
        public string MaxWindFoot { get; set; }
        [NotMapped]
        public string AvgWindFoot { get; set; }
        [NotMapped]
        public string MinWindFoot { get; set; }
        [NotMapped]
        public string MaxWindYard { get; set; }
        [NotMapped]
        public string AvgWindYard { get; set; }
        [NotMapped]
        public string MinWindYard { get; set; }
        [NotMapped]
        public string MaxWindKnot { get; set; }
        [NotMapped]
        public string AvgWindKnot { get; set; }
        [NotMapped]
        public string MinWindKnot { get; set; }



        public string MaxPress { get; set; }

        public string AvgPress { get; set; }

        public string MinPress { get; set; }
        [NotMapped]
        public string MaxPressAT { get; set; }
        [NotMapped]
        public string AvgPressAT { get; set; }
        [NotMapped]
        public string MinPressAT { get; set; }
        [NotMapped]
        public string MaxPressBAR { get; set; }
        [NotMapped]
        public string AvgPressBAR { get; set; }
        [NotMapped]
        public string MinPressBAR { get; set; }
        [NotMapped]
        public string MaxPressPSI { get; set; }
        [NotMapped]
        public string AvgPressPSI { get; set; }
        [NotMapped]
        public string MinPressPSI { get; set; }

        public string Season { get; set; }
    }
}
