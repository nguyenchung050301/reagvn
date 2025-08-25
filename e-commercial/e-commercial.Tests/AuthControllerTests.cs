using e_commercial.Controllers;
using e_commercial.DTOs.Request.User;
using e_commercial.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commercial.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public void Login_ReturnsBadRequest_WhenUsernameOrPasswordIsEmpty()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {   
                { "Jwt:PrivateKeyPath", "fake-key.pem" },
                { "Jwt:Issuer", "test-issuer" },
    
                { "Jwt:Audience", "test-audience" }
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            File.WriteAllText("fake-key.pem", "-----BEGIN PRIVATE KEY-----\nMIIB...\n-----END PRIVATE KEY-----");

            var mockUserService = new Mock<UserService>();
            var mockJwtService = new Mock<JWTService>(config);
            var controller = new AuthController(mockJwtService.Object, mockUserService.Object);

            // Test username empty
            var result1 = controller.Login(new UserLoginDTO { Username = "", Userpassword = "123" });
            Assert.IsType<BadRequestObjectResult>(result1);

            // Test password empty
            var result2 = controller.Login(new UserLoginDTO { Username = "user", Userpassword = "" });
            Assert.IsType<BadRequestObjectResult>(result2);
        }
    }
}
