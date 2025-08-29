using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.EntityFrameworkCore;
using e_commercial.Data;
using Microsoft.Extensions.DependencyInjection;
using e_commercial.Constants;
using System.Net.Http.Json;
using e_commercial.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
namespace e_commercial.Tests.Integration_Test
{
    public class LaptopApiTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        public LaptopApiTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_Then_Get_Product_Works()
        {
            //Start app ASP.Net Core in memory
            var client = _factory.CreateClient();

            //Seeding category and manufacturer
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ReagvnContext>(); //ref DBcontext

            context.Categories.Add(new Category
            {
                CategoryId = "2d02d00f8-5d42-11f0-b178-00d8618add37",
                CategoryName = "Test Category",
    
            });

            context.Manufacturers.Add(new Manufacturer
            {
                ManufacturerId = "2d24d4e3-5d42-11f0-b178",
                ManufacturerName = "Test Manufacturer",
            });
            context.SaveChanges();

            Laptop newLaptop = new Laptop
            {
                LaptopName = "Test Laptop",
                Price = 1500,
                LaptopDescription = "This is a test laptop",
                LaptopImage = JsonSerializer.Serialize(new List<string> { "image1.jpg", "image2.jpg" }),
                LaptopSize = int.Parse(LaptopSizeEnum.Inch27.ToString().Substring("Inch".Length).Trim()),
                CategoryId = "2d02d00f8-5d42-11f0-b178-00d8618add37",
                ManufacturerId = "2d24d4e3-5d42-11f0-b178"
            };

            var postResponse = await client.PostAsJsonAsync("/api/Laptop", newLaptop);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync("/api/Laptop");
            getResponse.EnsureSuccessStatusCode();

            var laptops = await getResponse.Content.ReadFromJsonAsync<List<Laptop>>();
            Assert.Contains(laptops, p => p.LaptopName == newLaptop.LaptopName);

        }
    }
}
