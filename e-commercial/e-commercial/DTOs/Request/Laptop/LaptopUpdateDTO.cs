using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Laptop
{
    public class LaptopUpdateDTO
    {
        [StringLength(255)]
        public string? LaptopName { get; set; }
        [Range(0, 100)]
        public float? LaptopSize { get; set; }
        [StringLength(255)]
        public string? LaptopDescription { get; set; }

        public List<string>? LaptopImage { get; set; }
        public string CategoryId { get; set; }
        public string ManufacturerId { get; set; }
    }
}
