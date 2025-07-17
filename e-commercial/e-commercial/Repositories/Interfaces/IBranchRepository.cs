using e_commercial.DTOs.Request;
using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        Branch GetByID(Guid id);
        IEnumerable<Branch> GetAll();
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(Guid id);
        void Delete(Branch branch);
    }
}
