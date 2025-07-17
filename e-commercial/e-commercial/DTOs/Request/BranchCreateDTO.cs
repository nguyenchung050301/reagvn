using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class BranchCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string? BranchName { get; set; }
        [Required]
        [StringLength(255)]
        public string? BranchAddress { get; set; }

        public string? UserId { get; set; }
    }
}
