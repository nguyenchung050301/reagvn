using e_commercial.DTOs.Request;
using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetByID(Guid id);
        IEnumerable<Category> GetAll();
        void Add(Category category);
        void Update(Category category);
        void Delete(Guid id);
        void Delete(Category category);
    }
}
