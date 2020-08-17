using System.ComponentModel.DataAnnotations;

namespace Wall.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get ; set; }
    }
}
