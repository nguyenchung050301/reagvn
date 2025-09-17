using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.User
{
    public class UserVerificationDTO
    {
        [Required]
        [StringLength(255)]
        public string UserMail { get; set; }

        [Required]
        [StringLength(6)]
        public string UserOtp { get; set; }

    }
}
