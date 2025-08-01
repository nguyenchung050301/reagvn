using e_commercial.DTOs.Request.Cart;
using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories;
using e_commercial.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace e_commercial.Services
{
    public class LaptopService
    {
        private readonly string productType = "Laptop";
        private readonly ILaptopRepository _laptopRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly JWTService _jwtService;
        private readonly CartService _cartService;

        public LaptopService(ILaptopRepository laptopRepository, ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository, CartService cartService, JWTService jwtService)
        {
            _laptopRepository = laptopRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _cartService = cartService;
            _jwtService = jwtService;
        }
        public LaptopDetailDTO GetLaptopDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }
            var laptop = _laptopRepository.GetByID(id);
            if (laptop == null)
            {
                throw new KeyNotFoundException($"Laptop with ID {id} not found.");
            }
            return new LaptopDetailDTO
            {

                LaptopName = laptop.LaptopName,
                LaptopSize = laptop.LaptopSize,
                LaptopDescription = laptop.LaptopDescription,
                LaptopImage = laptop.LaptopImage,
                CategoryName = laptop.Category?.CategoryName,
                ManufacturerName = laptop.Manufacturer?.ManufacturerName,
            };
        }
        public IEnumerable<LaptopDetailDTO> GetAllLaptopDetails(string token)
        {
            var handler = _jwtService.GetJwtSecurityTokenHandler();
            if (token.IsNullOrEmpty())
            {
                throw new BadValidationException("Token ko the null", nameof(token));
            }
            var tokenReader = handler.ReadToken(token);
       
            var laptops = _laptopRepository.GetAll();
            return laptops.Select(laptop => new LaptopDetailDTO
            {
                LaptopName = laptop.LaptopName,
                LaptopSize = laptop.LaptopSize,
                LaptopDescription = laptop.LaptopDescription,
                LaptopImage = laptop.LaptopImage,
                CategoryName = laptop.Category?.CategoryName,
                ManufacturerName = laptop.Manufacturer?.ManufacturerName,
            });
        }

        /// <summary>
        /// logic xu ly phan create laptop:
        /// b1: kiem tra khoa ngoai ManufacturerId, CategoryId
        /// b2: truong hop ko tim thay thi tra ve loi 400 bad request
        /// b3: neu tim thay thi tao moi laptop object
        /// b4: save changes
        /// </summary>
        /// <param name="laptopDTO"></param>
        public void CreateLaptop(LaptopCreateDTO laptopDTO)
        {

            var manufacturer = _manufacturerRepository.GetByID(Guid.Parse(laptopDTO.ManufacturerId));
            if (manufacturer == null)
            {
                throw new BadValidationException("ManufacturerId cannot be null.", nameof(manufacturer));
            }

            var category = _categoryRepository.GetByID(Guid.Parse(laptopDTO.CategoryId));
            if (category == null)
            {
                throw new BadValidationException("CategoryId cannot be null.", nameof(category));
            }

            var laptop = new Laptop
            {
                LaptopId = Guid.NewGuid().ToString(),
                LaptopName = laptopDTO.LaptopName,
                LaptopSize = laptopDTO.LaptopSize,
                LaptopDescription = laptopDTO.LaptopDescription,
                LaptopImage = JsonSerializer.Serialize(laptopDTO.LaptopImage),
                CategoryId = laptopDTO.CategoryId,
                ManufacturerId = laptopDTO.ManufacturerId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System", // This should be replaced with the actual user ID or name
            };
            _laptopRepository.Add(laptop);

        }
        public void UpdateLaptop(LaptopUpdateDTO laptopUpdateDTO, Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException("Id cannot be null.");
            }
            var laptop = _laptopRepository.GetByID(Id);
            if (laptop == null)
            {
                throw new ArgumentNullException("Laptop not found.");
            }
            laptop.LaptopName = laptopUpdateDTO.LaptopName;
            laptop.LaptopSize = laptopUpdateDTO.LaptopSize;
            laptop.LaptopDescription = laptopUpdateDTO.LaptopDescription;
            laptop.LaptopImage = JsonSerializer.Serialize(laptopUpdateDTO.LaptopImage);
            laptop.CategoryId = laptopUpdateDTO.CategoryId;
            laptop.ManufacturerId = laptopUpdateDTO.ManufacturerId;
            laptop.UpdatedAt = DateTime.UtcNow;
            laptop.UpdatedBy = "System"; // This should be replaced with the actual user ID or name
            _laptopRepository.Update(laptop);
        }
        public void DeleteLaptop(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("Id cannot be null.");
            }
            var laptop = _laptopRepository.GetByID(id);
            if (laptop == null)
            {
                throw new ArgumentNullException("Laptop not found.");
            }
            _laptopRepository.Delete(laptop);
        }
        public PaginationResponseDTO<LaptopItemDTO> GetPagination(PaginationRequestDTO requestDTO, string? name)
        {           
       /*     if (requestDTO.PageNumber < 1)
            {
                throw new BadValidationException("Page number must be greater than zero.", nameof(requestDTO.PageNumber));
            }
            if (requestDTO.PageSize < 1)
            {
                throw new BadValidationException("Page size must be greater than zero.", nameof(requestDTO.PageSize));
            }*/

            var items = _laptopRepository.GetPagination(requestDTO.PageNumber, requestDTO.PageSize, name);
            int count = items.Item2;

            return new PaginationResponseDTO<LaptopItemDTO>
            {
                CurrentPage = requestDTO.PageNumber,

                PageSize = requestDTO.PageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling((double)count / requestDTO.PageSize),
                Items = items.Item1.Select(laptop => new LaptopItemDTO
                {
                    Id = laptop.LaptopId,
                    Name = laptop.LaptopName,

                }).ToList()
            };

        }
        public void AddProductToCart(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadValidationException("Product ID cannot be empty.", nameof(id));
            }

            var existingProduct = _laptopRepository.GetByID(id);
            new CartAddProductDTO
            {
                ProductId = existingProduct.LaptopId,
                ProductType = productType,
                Quantity = 1, // Assuming default quantity is 1 when adding to cart
                UnitPrice = (float)existingProduct.Price
            };

            _cartService.AddToCart(new Cart
            {
                CartId = Guid.NewGuid().ToString(),
                ProductId = existingProduct.LaptopId.ToString(),
                ProductType = productType,
                Quantity = 1, // Assuming default quantity is 1 when adding to cart
                UnitPrice = (float)existingProduct.Price
            });
        }




    }
}
