using e_commercial.DTOs.Request;
using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        Manufacturer GetByID(Guid id);
        IEnumerable<Manufacturer> GetAll();
        void Add(Manufacturer manufacturer);
        void Update(Manufacturer manufacturer);
        void Delete(Guid id);
        void Delete(Manufacturer manufacturer);
    }
}
