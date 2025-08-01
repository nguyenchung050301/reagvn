using e_commercial.Data;
using e_commercial.DTOs.Request;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Models.Products;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Repositories
{
    public class KeyboardRepository : IKeyboardRepository
    {
        private readonly DbSet<Keyboard> _dbSet;
        private readonly ReagvnContext _context;
        public KeyboardRepository(ReagvnContext context)
        {
            _context = context;
            _dbSet = context.Keyboards;
        }

        public void Add(Keyboard keyboard)
        {          
            _dbSet.Add(keyboard);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var keyboard = FindByID(id);
            _dbSet.Remove(keyboard);
            _context.SaveChanges();
        }
        public void Delete(Keyboard keyboard)
        {
            _dbSet.Remove(keyboard);
            _context.SaveChanges();
        }
        public IEnumerable<Keyboard> GetAll()
        {
            return _dbSet.Include(p => p.Category).Include(p => p.Manufacturer).ToList();
        }

        public Keyboard GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }

            var keyboard = _dbSet
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefault(p => p.KeyboardId == id.ToString());

            if (keyboard == null)
            {
                throw new KeyNotFoundException($"Keyboard with ID {id} not found.");
            }

            return keyboard;
        }

        public PaginationResponseDTO<Keyboard> GetPagination(PaginationRequestDTO requestDTO)
        {
            var query = _dbSet.AsQueryable();
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / requestDTO.PageSize);
            var items = query.Skip(requestDTO.PageSize * (requestDTO.PageNumber - 1)).
                OrderByDescending(p => p.KeyboardName).Take(requestDTO.PageSize).ToList();

            return new PaginationResponseDTO<Keyboard>
            {
                CurrentPage = requestDTO.PageNumber,
                PageSize = requestDTO.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = items
            };
        }

        public void Update(Keyboard keyboard)
        {
            _dbSet.Update(keyboard);
            _context.SaveChanges();
        }

        private Keyboard FindByID(Guid id)
        {
            var existing = _dbSet.Find(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Keyboard with ID {id} not found.");
            }
            return existing;
        }
    }
}
