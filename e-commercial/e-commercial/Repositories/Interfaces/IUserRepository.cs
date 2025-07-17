using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetByID(Guid id);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(Guid id);
        void Delete(User user);
    }
}
