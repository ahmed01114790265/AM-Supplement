using System.ComponentModel.DataAnnotations;

namespace AM_Supplement.Presentation.ViewsModel.AccountViewModel
{
    public class ChangePasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
       public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
