using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Request.Product;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.DTOs.Response.Pagination;

namespace e_commercial.Services.Interfaces
{
    public interface ILaptopService
    {
        public LaptopDetailDTO GetLaptopDetails(Guid id);
        public IEnumerable<LaptopDetailDTO> GetAllLaptopDetails();
        public void CreateLaptop(LaptopCreateDTO laptopDTO);
        public void UpdateLaptop(LaptopUpdateDTO laptopUpdateDTO, Guid Id);
        public void DeleteLaptop(Guid id);
        public PaginationResponseDTO<LaptopItemDTO> GetPagination(PaginationRequestDTO requestDTO, string? name);
        public IEnumerable<LaptopDetailDTO> ProductFilter(LaptopProductFilterDTO requestDTO);
    }
}
