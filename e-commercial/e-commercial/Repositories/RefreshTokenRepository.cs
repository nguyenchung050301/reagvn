using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Refreshtoken> _dbSet;
        public RefreshTokenRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Refreshtokens;
        }
        public void Add(Refreshtoken refreshtoken)
        {
            _dbSet.Add(refreshtoken);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(FindByID(id));
            _context.SaveChanges();
        }

        public void Delete(Refreshtoken refreshtoken)
        {
            _dbSet.Remove(refreshtoken);
            _context.SaveChanges();
        }

        public IEnumerable<Refreshtoken> GetAll()
        {
            return _dbSet
                .Include(u => u.User)
                .ToList();
        }

        public Refreshtoken GetByID(Guid id)
        {
            var existing = _dbSet
                .FirstOrDefault(p => p.TokenId == id.ToString());    
            if (existing == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return existing;
        }

        public void Update(Refreshtoken refreshtoken)
        {
            _dbSet.Update(refreshtoken);
            _context.SaveChanges();
        }
        private Refreshtoken FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Refreshtoken with ID {id} not found.");
            }
            return existing;
        }
    }
}
