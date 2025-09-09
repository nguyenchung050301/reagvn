using e_commercial.Constants;
using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Product
{
    public class LaptopProductFilterDTO
    {
        [Range(0, int.MaxValue)]
        public int? minPrice { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int? maxPrice { get; set; } = int.MaxValue;

        [StringLength(255)]
        public string? manufacturerName { get; set;}


        public LaptopSizeEnum? size { get; set; }
    }
}
