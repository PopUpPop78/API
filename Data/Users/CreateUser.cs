using System.ComponentModel.DataAnnotations;

namespace Data.Users
{
    public class CreateUser : LoginUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}