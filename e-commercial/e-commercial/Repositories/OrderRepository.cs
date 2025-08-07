using e_commercial.Constants;
using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Order> _dbSet;
      //  private readonly DbSet<OrderDetail> _orderDetailDbSet;
        public OrderRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Set<Order>();
        }

        public Order GetByID(Guid id) => FindByID(id);
        public IEnumerable<Order> GetAll() => _dbSet.ToList();
        public void Add(Order order)
        {
            _dbSet.Add(order);
            _context.SaveChanges();
        }
        public void Update(Order order)
        {
            _dbSet.Update(order);
            _context.SaveChanges();
        }
        public void Delete(Order order)
        {
            _dbSet.Remove(order);
            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var existing = FindByID(id);   
            _dbSet.Remove(existing);
            _context.SaveChanges();
        }
        public (IQueryable<Order>, int) GetPagination(int pageNumber, int pageSize, OrderStatusEnum status)
        {
            var query = SearchByStatus(status);
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling((float)totalCount / pageSize);
            return (query.Skip(pageSize * (pageNumber -1)).OrderBy(p => p.CreatedAt).Take(pageSize), totalCount);
        }
        public IQueryable<Order> SearchByStatus(OrderStatusEnum status)
        {
            if (string.IsNullOrEmpty(status.ToString()))
            {
                return _dbSet.OrderByDescending(p => p.CreatedAt);
            }
            return _dbSet.Where(p => p.OrderStatus == status.ToString())
                         .OrderByDescending(p => p.CreatedAt);
                       
        }
        private Order FindByID(Guid id)
        {
            var existing = _dbSet.Find(id.ToString());
            if (existing == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
            return existing;
        }
    }
}