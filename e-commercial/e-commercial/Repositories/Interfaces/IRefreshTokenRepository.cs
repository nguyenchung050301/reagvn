using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Refreshtoken GetByID(Guid id);
        IEnumerable<Refreshtoken> GetAll();
        void Add(Refreshtoken refreshtoken);
        void Update(Refreshtoken refreshtoken);
        void Delete(Guid id);
        void Delete(Refreshtoken refreshtoken);
    }
}
