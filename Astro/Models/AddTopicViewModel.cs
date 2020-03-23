using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Astro.Models
{
    public class AddTopicViewModel
    {
        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name = "Tytuł")]
        [MinLength(10, ErrorMessage = "Wymagan długość to 10 znaków")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name = "Zawartość")]
        public string Comment { get; set; }
    }
}
