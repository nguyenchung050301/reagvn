using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class CategoryCreateDTO
    {
        [Required]
        [StringLength(255)]

        public string? CategoryName { get; set; }

       

    }
}
