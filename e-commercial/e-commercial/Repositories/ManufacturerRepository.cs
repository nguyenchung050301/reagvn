using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DbSet<Manufacturer> _dbSet;
        private readonly ReagvnContext _context;
        public ManufacturerRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Manufacturers;
        }
        public void Add(Manufacturer manufacturer)
        {          
            _dbSet.Add(manufacturer);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var manufacturer = FindByID(id);          
            _dbSet.Remove(manufacturer);
            _context.SaveChanges();
        }
        public void Delete(Manufacturer manufacturer)
        {
            _dbSet.Remove(manufacturer);
            _context.SaveChanges();
        }
        public IEnumerable<Manufacturer> GetAll()
        {
            return _dbSet.ToList();
        }

        public Manufacturer GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }
            Manufacturer manufacturer = _dbSet
                .FirstOrDefault(p => p.ManufacturerId == id.ToString());
            if (manufacturer == null)
            {
                throw new KeyNotFoundException($"Laptop with ID {id} not found.");
            }
            return manufacturer;
        }

        public void Update(Manufacturer manufacturer)
        {
            _dbSet.Update(manufacturer);
            _context.SaveChanges();
        }
        private Manufacturer FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Manufacturer with ID {id} not found.");
            }
            return existing;
        }
    }
}
