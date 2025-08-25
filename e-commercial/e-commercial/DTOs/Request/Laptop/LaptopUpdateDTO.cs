using e_commercial.Constants;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace e_commercial.DTOs.Request.Laptop
{
    public class LaptopUpdateDTO
    {
        [StringLength(255)]
        public string? LaptopName { get; set; }
        [Range(0, 100)]
        public LaptopSizeEnum? LaptopSize { get; set; }
        [StringLength(255)]
        public string? LaptopDescription { get; set; }

        public List<string>? LaptopImage { get; set; }
        public string CategoryId { get; set; }
        public string ManufacturerId { get; set; }
        public int? StockQuantity { get; set; }

        public float? Price { get; set; }
    }
}
