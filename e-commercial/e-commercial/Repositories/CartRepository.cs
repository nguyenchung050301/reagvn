using e_commercial.Data;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Cart> _dbSet;
        public CartRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Carts;
        }

        public void Add(Cart cart)
        {
            _dbSet.Add(cart);
            _context.SaveChanges(); 
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(GetByID(id));
            _context.SaveChanges();
        }

        public void Delete(Cart cart)
        {
            _dbSet.Remove(cart);
            _context.SaveChanges();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _dbSet.ToList();
        }

        public Cart GetByID(Guid id)
        {

            return _dbSet.FirstOrDefault(c => c.ProductId == id.ToString());

        }

        public void Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
