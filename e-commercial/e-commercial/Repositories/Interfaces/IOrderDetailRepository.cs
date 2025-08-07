using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {
        Orderdetail GetByID(Guid id);
        IEnumerable<Orderdetail> GetAll();     
        void Add(Orderdetail orderDetail);
        void Update(Orderdetail orderDetail);
        void Delete(Orderdetail orderDetail);
        void Delete(Guid id);
        public IEnumerable<Orderdetail> GetProductsByOrderID(Guid orderId);
    }
}