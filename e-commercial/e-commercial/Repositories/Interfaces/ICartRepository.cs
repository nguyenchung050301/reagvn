using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart GetByID(Guid id);
        IEnumerable<Cart> GetAll();
        void Add(Cart cart);
        void Update(Cart cart);
        void Delete(Guid id);
        void Delete(Cart cart);

    }
}

