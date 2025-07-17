using e_commercial.Data;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbSet<Employee> _dbSet;
        private readonly ReagvnContext _context;
        public EmployeeRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Employees;
        }
        public void Add(Employee employee)
        {
            _dbSet.Add(employee);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(FindByID(id));
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _dbSet.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbSet.Include(p => p.Branch).Include(p => p.Department).ToList();
        }

        public Employee GetByID(Guid id)
        {
            var existing = _dbSet
                .Include(p => p.Branch)
                .Include(p => p.Department)
                .FirstOrDefault(p => p.EmployeeId == id.ToString());
            if (existing == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return existing;
        }

        public void Update(Employee employee)
        {
            _dbSet.Update(employee);
            _context.SaveChanges();
        }
        private Employee FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return existing;
        }
    }
}
