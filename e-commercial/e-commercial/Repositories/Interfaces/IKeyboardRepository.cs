using e_commercial.DTOs.Request;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Models.Products;

namespace e_commercial.Repositories.Interfaces
{
    public interface IKeyboardRepository
    {
        Keyboard GetByID(Guid id);
        IEnumerable<Keyboard> GetAll();
        void Add(Keyboard keyboard);
        void Update(Keyboard keyboard);
        void Delete(Guid id);
        PaginationResponseDTO<Keyboard> GetPagination(PaginationRequestDTO requestDTO);
    }
}
