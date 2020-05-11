using System.ComponentModel.DataAnnotations;

namespace Astro.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name = "Obecne hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Pole wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła muszą się zgadzać!")]
        public string ConfirmPassword { get; set; }
    }
}
