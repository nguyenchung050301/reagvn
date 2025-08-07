using e_commercial.Constants;
using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetByID(Guid id);
        IEnumerable<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        void Delete(Guid id);
        (IQueryable<Order>, int) GetPagination(int pageNumber, int pageSize, OrderStatusEnum status);
        IQueryable<Order> SearchByStatus(OrderStatusEnum status);
    }
}