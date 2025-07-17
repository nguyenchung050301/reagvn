using e_commercial.DTOs.Request;
using e_commercial.Models;

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
    }
}
