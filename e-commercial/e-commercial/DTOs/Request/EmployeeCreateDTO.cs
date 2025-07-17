using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class EmployeeCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string? EmployeeName { get; set; }
        [Required]
        [StringLength(4)]
        public string? EmployeeGender { get; set; }
        [Required]
        public string? BranchId { get; set; }
        [Required]
        public string? DepartmentId { get; set; }
    }
}
