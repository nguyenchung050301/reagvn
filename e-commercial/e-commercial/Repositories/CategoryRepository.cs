using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSet<Category> _dbSet;
        private readonly ReagvnContext _context;
        public CategoryRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Categories;
        }
        public void Add(Category category)
        {          
            _dbSet.Add(category);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var category = FindByID(id);          
            _dbSet.Remove(category);
            _context.SaveChanges();
        }
        public void Delete(Category category)
        {
            _dbSet.Remove(category);
            _context.SaveChanges();
        }
        public IEnumerable<Category> GetAll()
        {
            return _dbSet.ToList();
        }

        public Category GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }
            Category category = _dbSet
                .FirstOrDefault(p => p.CategoryId == id.ToString());
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return category;
        }

        public void Update(Category category)
        {
            _dbSet.Update(category);
            _context.SaveChanges();
        }
        private Category FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return existing;
        }
    }
}
