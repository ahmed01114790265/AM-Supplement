using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.DTO.AdminDasboard.AdminAccount
{
    // LoginDTO.cs
    public class LoginDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    // RegisterDTO.cs
    public class RegisterDTO
    {
        [Required] public string Username { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required][DataType(DataType.Password)] public string Password { get; set; }
        [Required][Compare("Password")][DataType(DataType.Password)] public string ConfirmPassword { get; set; }
    }

    // ChangePasswordDTO.cs
    public class ChangePasswordDTO
    {
        [Required][DataType(DataType.Password)] public string CurrentPassword { get; set; }
        [Required][DataType(DataType.Password)] public string NewPassword { get; set; }
        [Required][Compare("NewPassword")][DataType(DataType.Password)] public string ConfirmNewPassword { get; set; }
    }

}
