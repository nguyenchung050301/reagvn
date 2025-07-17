using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Response.Laptop
{
    public class LaptopAllDetailDTO
    {
        public string LaptopId { get; set; } = null!;
        public string? LaptopName { get; set; }
        public float? LaptopSize { get; set; }
        public string? LaptopDescription { get; set; }

        public string? LaptopImage { get; set; }

        public string? CategoryName { get; set; }

        public string? ManufacturerName { get; set; }
    }
}
