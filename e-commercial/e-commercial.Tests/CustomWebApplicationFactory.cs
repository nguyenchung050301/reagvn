using e_commercial.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace e_commercial.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //Clone an in memory DB, use program.cs in main solution
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ReagvnContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<ReagvnContext>(options =>
                    options.UseInMemoryDatabase("CloneDB"));


            });

            builder.UseEnvironment("Test");
            base.ConfigureWebHost(builder);
        }

        private void SeedData(ReagvnContext db)
        {
            // Seed initial data into the in-memory database
            db.Categories.Add(new e_commercial.Models.Category
            {
                CategoryId = "cat1",
                CategoryName = "Test Category",
            });
            db.Manufacturers.Add(new e_commercial.Models.Manufacturer
            {
                ManufacturerId = "manu1",
                ManufacturerName = "Test Manufacturer",
            });
            db.SaveChanges();
        }
    }
}
