using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class DepartmentRepository : IDepartmenRepository
    {
        private readonly ReagvnContext _context;
        private readonly DbSet<Department> _dbSet;
        public DepartmentRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Departments;
        }
        public void Add(Department branch)
        {
            _dbSet.Add(branch);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var existing = FindByID(id);
            _dbSet.Remove(existing);
            _context.SaveChanges();
        }

        public void Delete(Department branch)
        {
            _dbSet.Remove(branch);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbSet.ToList();
        }

        public Department GetByID(Guid id)
        {
            var exising = _dbSet
                .FirstOrDefault(p => p.DepartmentId == id.ToString());
            if (exising == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }
            return exising; 
        }

        public void Update(Department department)
        {
            _dbSet.Update(department);
            _context.SaveChanges();
        }
        private Department FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }
            return existing;
        }
    }
}
