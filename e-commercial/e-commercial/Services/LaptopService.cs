using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.Models;
using e_commercial.Repositories;
using e_commercial.Repositories.Interfaces;
using System.Text.Json;

namespace e_commercial.Services
{
    public class LaptopService
    {
        private readonly ILaptopRepository _laptopRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        public LaptopService(ILaptopRepository laptopRepository, ICategoryRepository categoryRepository, IManufacturerRepository manufacturerRepository)
        {
            _laptopRepository = laptopRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;   
        }
        public LaptopDetailDTO GetLaptopDetails(Guid id)
        {
            var laptop = _laptopRepository.GetByID(id);
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
        public IEnumerable<LaptopAllDetailDTO> GetAllLaptopDetails()
        {
            var laptops = _laptopRepository.GetAll();
            return laptops.Select(laptop => new LaptopAllDetailDTO
            {
                LaptopId = laptop.LaptopId,
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

            CheckID(laptopDTO.ManufacturerId);
            var manufacturer = _manufacturerRepository.GetByID(Guid.Parse(laptopDTO.ManufacturerId));
            if (manufacturer == null)
            {
                throw new ArgumentNullException("ManufacturerId cannot be null.");
            }

            CheckID(laptopDTO.CategoryId);
            var category = _categoryRepository.GetByID(Guid.Parse(laptopDTO.CategoryId));
            if (category == null)
            {
                throw new ArgumentNullException("CategoryId cannot be null.");
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
        private void CheckID(string id)
        {
            if (!Guid.TryParse(id, out _))
            {
                throw new ArgumentNullException(id+" cannot be null.");
            }
            
        }
    }
}
