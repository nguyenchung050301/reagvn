using e_commercial.Constants;
using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Pagination
{
    public class ProductPaginationRequestDTO
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
        public string name { get; set; }
    }
}
