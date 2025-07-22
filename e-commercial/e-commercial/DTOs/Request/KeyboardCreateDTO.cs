using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request
{
    public class KeyboardCreateDTO
    {
        //     public string KeyboardId { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string? KeyboardName { get; set; }
        
        [Required]
        [StringLength(255)]
        public string? KeyboardSwitch { get; set; }

        [StringLength(255)]
        public string? KeyboardDescription { get; set; }

        public string? KeyboardImage { get; set; }

        public string? CategoryId { get; set; }

        public string? ManufacturerId { get; set; }

     /*   public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }*/
    }
}
