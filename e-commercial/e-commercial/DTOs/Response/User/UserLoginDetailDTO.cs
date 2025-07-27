using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Response.User
{
    public class UserLoginDetailDTO
    {
        [StringLength(255)]
        public string? Username { get; set; }
      
        [StringLength(255)]
        public string? UserShownname { get; set; }

        public string? UserDistrict { get; set; }
   
        public string? UserWard { get; set; }
     
        [StringLength(255)]
        public string? UserAddress { get; set; }
     
        [StringLength(255)]
        public string? UserPhone { get; set; }

        [StringLength(255)]
        public string? UserEmail { get; set; }
    }
}
