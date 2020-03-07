using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Astro.Models
{
    public class LoginViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name="Adres email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
