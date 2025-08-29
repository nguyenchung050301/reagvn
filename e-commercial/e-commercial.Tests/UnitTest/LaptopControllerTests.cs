using AutoMapper;
using e_commercial.Constants;
using e_commercial.Controllers.Admin;
using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Request.Product;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using e_commercial.Services;
using e_commercial.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace e_commercial.Tests
    {
        public class LaptopControllerTests
        {
            [Fact]
            public void Filter_ReturnsOkResult_WhenCalled()
            {
                // Arrange
                var mockService = new Mock<ILaptopService>();
                var fakeResult = new List<LaptopDetailDTO>
                {
                    new LaptopDetailDTO { LaptopName = "asd", LaptopPrice = 2000 },
                }.AsEnumerable();

                // Giả lập service trả về fakeResult khi gọi ProductFilter
           

                var controller = new LaptopController(mockService.Object);

                var filterDto = new LaptopProductFilterDTO
                {
                    manufacturerName = "Dell",
                    minPrice = 1000,
                    maxPrice = 2000
                };

            mockService.Setup(s => s.ProductFilter(It.Is<LaptopProductFilterDTO>(p => p == filterDto)))
                      .Returns(fakeResult);

            // Act
            var result = controller.Filter(filterDto);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(fakeResult, okResult.Value);
            }
        [Fact]
        public void CreateProduct_OkResult()
        {
            var mockRepo = new Mock<ILaptopRepository>();
            var mockService = new Mock<ILaptopService>();
            var controller = new LaptopController(mockService.Object);
            var dto = new LaptopCreateDTO
            {
                LaptopDescription = "asd",
                LaptopImage = new List<string>{"a", "b"},
                LaptopName = "Dell",
                LaptopSize = LaptopSizeEnum.Inch20,
                CategoryId = "2d02d00f8-5d42-11f0-b178-00d8618add37",
                ManufacturerId = "2d24d4e3-5d42-11f0-b178-00d8618add37"
            };

            //Act
            var result = controller.Create(dto);
            

            //Assert
            mockService.Verify(s => s.CreateLaptop(It.Is<LaptopCreateDTO>(p => 
                p.LaptopName == dto.LaptopName &&
                p.LaptopSize == dto.LaptopSize &&
                p.CategoryId == dto.CategoryId &&
                p.ManufacturerId == dto.ManufacturerId &&
                p.LaptopImage != null && dto.LaptopImage != null &&
                p.LaptopImage.SequenceEqual(dto.LaptopImage) &&
                p.LaptopDescription == dto.LaptopDescription 
                )), Times.Once);

            var statusResult = Assert.IsType<CreatedResult>(result);

   
        }
        }
    }


