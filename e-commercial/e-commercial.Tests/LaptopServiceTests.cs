using AutoMapper;
using e_commercial.Constants;
using e_commercial.DTOs.Request.Laptop;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_commercial.Tests
{
    public class LaptopServiceTests
    {
        private readonly Mock<ILaptopRepository> _mockLaptopRepo = new();
        private readonly Mock<ICategoryRepository> _mockCategoryRepo = new();
        private readonly Mock<IManufacturerRepository> _mockManufacturerRepo = new();

        private readonly Mock<IMapper> _mockMapper = new();

        private readonly LaptopService _service;

        public LaptopServiceTests()
        {
            _service = new LaptopService(
                null, // context không dùng trong unit test
                _mockLaptopRepo.Object,
                _mockCategoryRepo.Object,
                _mockManufacturerRepo.Object,

                _mockMapper.Object
            );
        }

        [Fact]
        public void CreateLaptop_CallsRepository_ValidInput()
        {
            var dto = new LaptopCreateDTO
            {
                LaptopName = "Dell",
                Price = 2000,
                LaptopDescription = "Test laptop",
                LaptopImage = new List<string> { "a", "b" },
                LaptopSize = LaptopSizeEnum.Inch20,
                CategoryId = "2d02d00f8-5d42-11f0-b178-00d8618add37",
                ManufacturerId = "2d24d4e3-5d42-11f0-b178-00d8618add37",
            };

            var mappedEntity = new Laptop
            {
                LaptopId = Guid.NewGuid().ToString(),
                LaptopName = dto.LaptopName,
                Price = dto.Price,
                LaptopDescription = dto.LaptopDescription,
                LaptopImage = JsonSerializer.Serialize(dto.LaptopImage),
                LaptopSize = int.Parse(dto.LaptopSize.ToString().Substring("Inch".Length).Trim()),
                CategoryId = dto.CategoryId,
                ManufacturerId = dto.ManufacturerId,
            };
          
            _mockMapper.Setup(m => m.Map<Laptop>(It.IsAny<LaptopCreateDTO>())).Returns(mappedEntity);

            _service.CreateLaptop(dto);
            _mockMapper.Verify(m => m.Map<Laptop>((It.IsAny<LaptopCreateDTO>())), Times.Once);
            _mockLaptopRepo.Verify(r => r.Add(mappedEntity), Times.Once);

        }
    }
}
