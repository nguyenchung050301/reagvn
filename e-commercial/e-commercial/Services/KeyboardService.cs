
using AutoMapper;
using e_commercial.Constants;
using e_commercial.Data;
using e_commercial.DTOs.Request.Keyboard;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Keyboard;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services.ParentService;


namespace e_commercial.Services
{
    public class KeyboardService
    {
        private readonly IMapper _mapper;
        private readonly string productType = "Keyboard";
        private readonly IKeyboardRepository _keyboardRepository;
        protected readonly IManufacturerRepository _manufacturerRepository;
        protected readonly ICategoryRepository _categoryRepository;

        public KeyboardService(ReagvnContext context, IKeyboardRepository keyboardRepository, 
            IManufacturerRepository manufacturerRepository, ICategoryRepository categoryRepository, IMapper mapper)          
        {
            _mapper = mapper;
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;
            _keyboardRepository = keyboardRepository;
        }
        public KeyboardDetailDTO GetKeyboardDetails(Guid id)
        {
            var existing = _keyboardRepository.GetByID(id);

            if (existing == null)
            {
                throw new KeyNotFoundException($"Keyboard with ID {id} not found.");
            }

            return _mapper.Map<KeyboardDetailDTO>(existing);
        }

        public IEnumerable<KeyboardDetailDTO> GetKeyboardAllDetails()
        {
            var keyboards = _keyboardRepository.GetAll();
            return _mapper.Map<IEnumerable<KeyboardDetailDTO>>(keyboards);
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
            var keyboard = _mapper.Map<Keyboard>(keyboardDTO);
            keyboard.KeyboardId = Guid.NewGuid().ToString();
            keyboard.CreatedAt = DateTime.Now;
            keyboard.CreatedBy = RoleEnum.Admin;
            _keyboardRepository.Add(keyboard);
        }

        public void UpdateKeyboard(KeyboardUpdateDTO keyboardDTO, Guid id)
        {
            if (id == null)
            {
                throw new BadValidationException("ID cannot be null.", nameof(id));
            }
            var existing = _keyboardRepository.GetByID(id);
            if (existing == null)
            {
                throw new BadValidationException($"Keyboard with ID {id} not found.", nameof(existing));
            }
           /* existing.KeyboardName = keyboardDTO.KeyboardName;
            existing.KeyboardDescription = keyboardDTO.KeyboardDescription;
            existing.KeyboardImage = keyboardDTO.KeyboardImage;
            existing.KeyboardSwitch = keyboardDTO.KeyboardSwitch;
            existing.CategoryId = keyboardDTO.CategoryId;
            existing.ManufacturerId = keyboardDTO.ManufacturerId;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = "System"; // This should be replaced with the actual user ID or name*/
            var keyboard = _mapper.Map<Keyboard>(existing);
            keyboard.UpdatedBy = RoleEnum.Admin;
            keyboard.UpdatedAt = DateTime.Now;
            _keyboardRepository.Update(existing);
        }

        public void DeleteKeyboard(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadValidationException("ID cannot be empty.", nameof(id));
            }

            _keyboardRepository.Delete(id);
        }

        public PaginationResponseDTO<KeyboardItemDTO> GetPagination(PaginationRequestDTO requestDTO, string? name)
        {
            var item = _keyboardRepository.GetPagination(requestDTO.PageNumber, requestDTO.PageSize, name);
            int totalCount = item.Item2;
            return new PaginationResponseDTO<KeyboardItemDTO>
            {
                CurrentPage = requestDTO.PageNumber,
                PageSize = requestDTO.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / requestDTO.PageSize),
                Items = item.Item1.Select(p => new KeyboardItemDTO
                {
                    Id = p.KeyboardId,
                    Name = p.KeyboardName,
                }
                ).ToList()
            };
        }
    }
}
