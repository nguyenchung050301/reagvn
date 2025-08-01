using e_commercial.DTOs.Request;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Models.Products;

namespace e_commercial.Repositories.Interfaces
{
    public interface ILaptopRepository
    {
        Laptop GetByID(Guid id);
        IEnumerable<Laptop> GetAll();
        void Add(Laptop laptop);
        void Update(Laptop laptop);
        void Delete(Guid id);
        void Delete(Laptop laptop);
        (IQueryable<Laptop>,int) GetPagination(int pageNumber, int pageSize, string? name);
        IQueryable<Laptop> SearchByName(string name);
    }
}
