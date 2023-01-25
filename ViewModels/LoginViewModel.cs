using System.ComponentModel.DataAnnotations;

namespace MVCproject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email adress is required")]
        [Display(Name = "Email Addresss")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
