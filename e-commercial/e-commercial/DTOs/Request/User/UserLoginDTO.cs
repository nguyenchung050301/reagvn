using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.User
{
    public class UserLoginDTO
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Userpassword { get; set; }

    }
}
