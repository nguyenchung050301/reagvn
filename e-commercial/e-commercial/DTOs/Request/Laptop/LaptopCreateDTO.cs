using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Laptop
{
    public class LaptopCreateDTO
    {
     //   public string LaptopId { get; set; } = null!;
        [StringLength(255)]
        [Required]
        public string? LaptopName { get; set; }
        [Required]
        [Range(0, 100)]
        public float? LaptopSize { get; set; }
        [Required]
        [StringLength(255)]
        public string? LaptopDescription { get; set; }

        public List<string>? LaptopImage { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string ManufacturerId { get; set; }

      /*  public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }*/
    }
}
