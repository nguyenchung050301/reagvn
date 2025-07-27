using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Exceptions;
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
           
            Laptop laptop = _dbSet
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefault(p => p.LaptopId == id.ToString());
          
            return laptop;
        }

        public (IQueryable<Laptop>,int) GetPagination(int pageNumber, int pageSize, string? name)
        {
            // var query = _dbSet
            //   .Include(p => p.Category)
            //   .Include(p => p.Manufacturer)
            //    .AsQueryable(); //convert Ienumerable to IQueryable 
            var query = SearchByName(name);
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);  

            var items  = query.Skip(pageSize * (pageNumber - 1)).
                OrderByDescending(p => p.LaptopName).Take(pageSize);
            return (items, totalCount);
           
        }

        public IQueryable<Laptop> SearchByName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return _dbSet.AsQueryable();
            }
            return _dbSet.Where(p => p.LaptopName.Contains(name)).OrderBy(p => p.LaptopName); ;
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
