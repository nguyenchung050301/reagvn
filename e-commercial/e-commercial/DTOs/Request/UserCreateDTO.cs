using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class UserCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string? Username { get; set; }
        [Required]
        [StringLength(255)]
        public string? Userpassword { get; set; }
        [Required]
        public string? UserRole { get; set; }

    }
}
