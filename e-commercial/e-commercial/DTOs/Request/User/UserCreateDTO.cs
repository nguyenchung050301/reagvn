using e_commercial.Models;
using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.User
{
    public class UserCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string? Username { get; set; }
        [Required]
        [StringLength(255)]
        public string? Userpassword { get; set; }

       // public string? UserRole { get; set; } = "User";
        [Required]
        [StringLength(255)]
        public string? UserShownname { get; set; }
        [Required]
        public string? UserDistrict { get; set; }
        [Required]
        public string? UserWard { get; set; }
        [Required]
        [StringLength(255)]
        public string? UserAddress { get; set; }
        [Required]
        [StringLength(255)]
        public string? UserPhone { get; set; }
     //   [Required]
        [StringLength(255)]
        public string? UserEmail { get; set; }

     //   public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    }
}
