using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace e_commercial.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Orderdetail> _dbSet;
        public OrderDetailRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Set<Orderdetail>();
        }

        public void Add(Orderdetail orderDetail)
        {
            _dbSet.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Delete(Orderdetail orderDetail)
        {
            _dbSet.Remove(orderDetail);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var existing = FindByID(id);
            _dbSet.Remove(existing);
            _context.SaveChanges();
        }

        public IEnumerable<Orderdetail> GetAll()
        {
            return _dbSet
                .ToList();
        }

        public Orderdetail GetByID(Guid id)
        {
            return FindByID(id);
        }

       public IEnumerable<Orderdetail> GetProductsByOrderID(Guid orderId)
       {
            return _dbSet.Where(od => od.OrderId == orderId.ToString()).ToList();
       }

        public void Update(Orderdetail orderDetail)
        {
            _dbSet.Update(orderDetail);
            _context.SaveChanges();
        }
        private Orderdetail FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Order detail with ID {id} not found.");
            }
            return existing;
        }
    }
}