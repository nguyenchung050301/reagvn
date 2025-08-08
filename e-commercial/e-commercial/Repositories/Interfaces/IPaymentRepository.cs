using e_commercial.Models;

namespace e_commercial.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Payment GetByID(Guid id);
        IEnumerable<Payment> GetAll();
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(Guid id);
        void Delete(Payment payment);
    }
}
