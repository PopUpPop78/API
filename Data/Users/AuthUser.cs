using System.ComponentModel.DataAnnotations;

namespace Data.Users
{
    public class AuthUser
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
