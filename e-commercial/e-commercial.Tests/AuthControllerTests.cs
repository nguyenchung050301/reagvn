using e_commercial.Constants;
using e_commercial.Controllers;
using e_commercial.DTOs.Request.User;
using e_commercial.Models;
using e_commercial.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace e_commercial.Tests
{
    public class AuthControllerTests
    {
     
 
     /*   [Fact]
        public void Login_ReturnsBadRequest_WhenUsernameOrPasswordIsEmpty()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {   
              //  { "Jwt:PrivateKeyPath", keysDir }, //no need
                { "Jwt:Issuer", "test-issuer" },
              //  { "Jwt:Expire", "60000" }, //maybe no need
                { "Jwt:Audience", "test-audience" }
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            User fakeUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = "testuser",
                Userpassword = "testpasswsord",
                UserRole = RoleEnum.User
            };

            var mockUserService = new Mock<UserService>();
            mockUserService.Setup(p => p.LoadByUserName(It.Is<UserLoginDTO>(p => p.Username == "testuser" && p.Userpassword == "testpasswsord")))
                .Returns(fakeUser);

            var mockJwtService = new Mock<JWTService>(config);
            mockJwtService.Setup(p => p.GenerateToken(It.Is<User>((p => p == "")))
                .Returns("fake-token");

            var controller = new AuthController(mockJwtService.Object, mockUserService.Object);

            // Test username empty
            var result1 = controller.Login(new UserLoginDTO { Username = "", Userpassword = "123" });
            Assert.IsType<BadRequestObjectResult>(result1);

            // Test password empty
            var result2 = controller.Login(new UserLoginDTO { Username = "user", Userpassword = "" });
            Assert.IsType<BadRequestObjectResult>(result2);
        }*/

    

       
    }
}
