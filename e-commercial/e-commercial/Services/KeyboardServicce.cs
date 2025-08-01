using e_commercial.DTOs.Request.Keyboard;
using e_commercial.DTOs.Response.Keyboard;
using e_commercial.Exceptions;
using e_commercial.Repositories.Interfaces;

namespace e_commercial.Services
{
    public class KeyboardServicce
    {
        private readonly IKeyboardRepository _keyboardRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICategoryRepository _categoryRepository;
        public KeyboardServicce(IKeyboardRepository keyboardRepository, IManufacturerRepository manufacturerRepository, ICategoryRepository categoryRepository)
        {
            _keyboardRepository = keyboardRepository;
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;
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
    }
}
