using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Pagination
{
    public class PaginationRequestDTO
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
