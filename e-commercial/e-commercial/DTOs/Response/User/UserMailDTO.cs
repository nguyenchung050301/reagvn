using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Response.User;

public class UserMailDTO
{
    [StringLength(255)]
    public string? Username { get; set; }
    [StringLength(255)]
    public string? UserEmail { get; set; }
    
    public string? UserOTP { get; set; }
    public bool isVerified { get; set; }
}