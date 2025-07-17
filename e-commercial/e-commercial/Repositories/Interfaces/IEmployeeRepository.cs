using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetByID(Guid id);
        IEnumerable<Employee> GetAll();
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Guid id);
        void Delete(Employee employee);
    }
}
