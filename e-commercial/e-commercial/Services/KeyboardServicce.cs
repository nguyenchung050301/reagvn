using e_commercial.DTOs.Request.Cart;
using e_commercial.DTOs.Request.Keyboard;
using e_commercial.DTOs.Response.Keyboard;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services.InterfaceService;

namespace e_commercial.Services
{
    public class KeyboardServicce : IProductService
    {
        private readonly string productType = "Keyboard";
     //   private readonly ICartRepository _cartRepository;
        private readonly IKeyboardRepository _keyboardRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly CartService _cartService;

        public KeyboardServicce(IKeyboardRepository keyboardRepository, IManufacturerRepository manufacturerRepository, 
            ICategoryRepository categoryRepository, CartService cartService)
        {
            _keyboardRepository = keyboardRepository;
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;
          //  _cartRepository = cartRepository;
            _cartService = cartService;
        }
        public KeyboardDetailDTO GetKeyboardDetails(Guid id)
        {
            var existing = _keyboardRepository.GetByID(id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"Keyboard with ID {id} not found.");
            }

            return new KeyboardDetailDTO
            {          
                KeyboardName = existing.KeyboardName,
                KeyboardDescription = existing.KeyboardDescription,
                KeyboardImage = existing.KeyboardImage,
                KeyboardSwitch = existing.KeyboardSwitch,
                CategoryName = existing.Category.CategoryName,
                ManufacturerName = existing.Manufacturer.ManufacturerName
            };
        }
        public IEnumerable<KeyboardAllDetailDTO> GetKeyboardAllDetails()
        {
            var keyboards = _keyboardRepository.GetAll();
            return keyboards.Select(k => new KeyboardAllDetailDTO
            {
                KeyboardName = k.KeyboardName,
                KeyboardDescription = k.KeyboardDescription,
                KeyboardImage = k.KeyboardImage,
                KeyboardSwitch = k.KeyboardSwitch,
                CategoryName = k.Category?.CategoryName,
                ManufacturerName = k.Manufacturer?.ManufacturerName
            });
        }
        public void CreateKeyboard(KeyboardCreateDTO keyboardDTO)
        {
            var category = _categoryRepository.GetByID(Guid.Parse(keyboardDTO.CategoryId));
            if (category == null) 
            {
                throw new BadValidationException("Category cannot be null", nameof(category));
            }

            var manufacturer = _manufacturerRepository.GetByID(Guid.Parse(keyboardDTO.ManufacturerId));
            if (manufacturer == null)
            {
                throw new BadValidationException("Manufacturer cannot be null", nameof(manufacturer));
            }
        }
        public void UpdateKeyboard(KeyboardUpdateDTO keyboardDTO)
        {

        }

        public void AddProductToCart(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadValidationException("Product ID cannot be empty.", nameof(id));
            }

            var existingProduct = _keyboardRepository.GetByID(id);
            new CartAddProductDTO
            {
                ProductId = existingProduct.KeyboardId,
                ProductType = productType,
                Quantity =  1, // Assuming default quantity is 1 when adding to cart
                UnitPrice = (float)existingProduct.Price
            };

            _cartService.AddToCart(new Cart
            {
                CartId = Guid.NewGuid().ToString(),
                ProductId = existingProduct.KeyboardId.ToString(),
                ProductType = productType,
                Quantity = 1, // Assuming default quantity is 1 when adding to cart
                UnitPrice = (float)existingProduct.Price
            });
        }

        public void DecreaseStock(Guid id, int quantity)
        {
            var existingProduct = _keyboardRepository.GetByID(id);
            if (existingProduct == null)
            {
                throw new BadValidationException($"Keyboard with ID {id} not found.", nameof(existingProduct));
            }

            existingProduct.StockQuantity -= quantity;
        }
    }
}
