using System.ComponentModel.DataAnnotations;

namespace AM_Supplement.Presentation.ViewsModel.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]   
        public string ConfirmPassword { get; set; }
    }
}
