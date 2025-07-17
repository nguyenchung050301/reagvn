using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class DepartmentCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string? DepartmentName { get; set; }

    }
}
