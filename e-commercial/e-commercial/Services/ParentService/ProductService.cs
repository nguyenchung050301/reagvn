using e_commercial.Data;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Models;
using e_commercial.Repositories;
using e_commercial.Repositories.Interfaces;
using System.Linq;

namespace e_commercial.Services.ParentService
{
    public class ProductService
    {
        protected readonly ReagvnContext _context;
        protected readonly IManufacturerRepository _manufacturerRepository;
        protected readonly ICategoryRepository _categoryRepository;
        public ProductService(ReagvnContext context, IManufacturerRepository manufacturerRepository, ICategoryRepository categoryRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public IQueryable<ProductSearchDTO> SearchProduct(string? text)
        {
            var keyboards = _context.Keyboards
                .Where(k => k.KeyboardName.Contains(text) || k.Category.CategoryName.Contains(text))
                .Select(p => new ProductSearchDTO
                {
                    ProductName = p.Category.CategoryName + " " + p.KeyboardName
                })
                .ToList();

            var laptops = _context.Laptops
                .Where(l => l.LaptopName.Contains(text) || l.Category.CategoryName.Contains(text))
                .Select(p => new ProductSearchDTO
                {
                    ProductName = p.Category.CategoryName + " " + p.LaptopName
                })
                .ToList();                             

            var result = keyboards.Union(laptops).ToList();
          //  Console.WriteLine("result:" + result);
            return result.AsQueryable();
        }

        public PaginationResponseDTO<ProductSearchDTO> GetPagination(ProductPaginationRequestDTO requestDTO)
        {
            var item = SearchProduct(requestDTO.name).Skip(requestDTO.PageSize * (requestDTO.PageNumber - 1))
                .Take(requestDTO.PageSize).OrderByDescending(p => p.ProductName);
            int totalCount = item.Count();
            return new PaginationResponseDTO<ProductSearchDTO>
            {
                CurrentPage = requestDTO.PageNumber,
                PageSize = requestDTO.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / requestDTO.PageSize),
                Items = item.Select(p => new ProductSearchDTO
                {
                    ProductName = p.ProductName,
                }
                ).ToList()
            };
        }
    }
}
