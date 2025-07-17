using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Users;
        }
        public void Add(User user)
        {
            _dbSet.Add(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(FindByID(id));
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _dbSet.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _dbSet
                .Include(u => u.Branches)
                .ToList();
        }

        public User GetByID(Guid id)
        {
            var existing = _dbSet
                .FirstOrDefault(p => p.UserId == id.ToString());    
            if (existing == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return existing;
        }

        public void Update(User user)
        {
            _dbSet.Update(user);
            _context.SaveChanges();
        }
        private User FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return existing;
        }
    }
}
