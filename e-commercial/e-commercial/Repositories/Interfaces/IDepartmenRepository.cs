using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IDepartmenRepository
    {
        Department GetByID(Guid id);
        IEnumerable<Department> GetAll();
        void Add(Department department);
        void Update(Department department);
        void Delete(Guid id);
        void Delete(Department department);
    }
}
