using System.ComponentModel.DataAnnotations;

namespace Data.Users
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Your password must be minimum {2} characters and maximum {1} characters", MinimumLength = 8)]
        public string Password { get; set; }
    }
}