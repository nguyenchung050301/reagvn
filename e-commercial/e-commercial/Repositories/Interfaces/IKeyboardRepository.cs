using e_commercial.DTOs.Request;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Keyboard;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IKeyboardRepository
    {
        Keyboard GetByID(Guid id);
        IEnumerable<Keyboard> GetAll();
        void Add(Keyboard keyboard);
        void Update(Keyboard keyboard);
        void Delete(Guid id);
        void Delete(Keyboard keyboard);
        (IQueryable<Keyboard>, int) GetPagination(int pageNumber, int pageSize, string? name);
        IQueryable<Keyboard> SearchByName(string? name);
        IEnumerable<Keyboard> FindInIDs(IEnumerable<string> ids);
    }
}
