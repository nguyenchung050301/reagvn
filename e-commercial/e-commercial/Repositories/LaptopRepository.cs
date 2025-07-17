using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly DbSet<Laptop> _dbSet;
        private readonly ReagvnContext _context;
        public LaptopRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Laptops;
        }
        public void Add(Laptop laptop)
        {          
            _dbSet.Add(laptop);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var laptop = FindByID(id);          
            _dbSet.Remove(laptop);
            _context.SaveChanges();
        }
        public void Delete(Laptop laptop)
        {
            _dbSet.Remove(laptop);
            _context.SaveChanges();
        }
        public IEnumerable<Laptop> GetAll()
        {
            return _dbSet.Include(p => p.Category).Include(p => p.Manufacturer).ToList();
        }

        public Laptop GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }
            Laptop laptop = _dbSet
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefault(p => p.LaptopId == id.ToString());
            if (laptop == null)
            {
                throw new KeyNotFoundException($"Laptop with ID {id} not found.");
            }
            return laptop;
        }

        public void Update(Laptop laptop)
        {
            _dbSet.Update(laptop);
            _context.SaveChanges();
        }
        private Laptop FindByID(Guid id)
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
