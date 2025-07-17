using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Branch> _dbSet;
        public BranchRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Branches;
        }
        public void Add(Branch branch)
        {
            _dbSet.Add(branch);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(FindByID(id));
            _context.SaveChanges();
        }
        public void Delete(Branch branch)
        {
            _dbSet.Remove(branch);
            _context.SaveChanges();
        }
        public IEnumerable<Branch> GetAll()
        {
            return _dbSet
                .Include(b => b.Employees)
                .Include(b => b.User)
                .ToList();
        }

        public Branch GetByID(Guid id)
        {
            var existing = _dbSet
                .Include(b => b.Employees)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BranchId == id.ToString());
            if (existing == null)
            {
                throw new KeyNotFoundException($"Branch with ID {id} not found.");
            }
            return existing;
        }

        public void Update(Branch branch)
        {
          
            _dbSet.Update(branch);
            _context.SaveChanges();

        }
        private Branch FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Laptop with ID {id} not found.");
            }
            return existing;
        }
    }
}
