using e_commercial.Data;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Payment> _dbSet;
        
        public PaymentRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = _context.Set<Payment>();
        }
        public void Add(Payment payment)
        {
            _dbSet.Add(payment);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var existing = FindByID(id);
            _dbSet.Remove(existing);
            _context.SaveChanges();
        }

        public void Delete(Payment payment)
        {
            _dbSet.Remove(payment);
            _context.SaveChanges();
        }

        public IEnumerable<Payment> GetAll()
        {
            return _dbSet.Include(p => p.Order)
                .Include(p => p.User)
                .ToList();
        }

        public Payment GetByID(Guid id)
        {
            var existing = _dbSet.Find(id.ToString());
            if (existing == null)
            {
                throw new BadValidationException($"Payment with ID {id} not found.");
            }
            return existing;
        }

        public void Update(Payment payment)
        {
            throw new NotImplementedException();
        }
        private Payment FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Payment with ID {id} not found.");
            }
            return existing;
        }
    }
}
