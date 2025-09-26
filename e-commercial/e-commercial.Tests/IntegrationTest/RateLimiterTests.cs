using System.Net.Http.Json;
using System.Threading.RateLimiting;
using e_commercial.DTOs.Request.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace e_commercial.Tests;

public class RateLimiterTests : IClassFixture<CustomWebApplicationFactory>
{
    private CustomWebApplicationFactory _factory;

    public RateLimiterTests()
    {
        _factory = new CustomWebApplicationFactory();
    }


    [Fact]
    public async Task CheckRateLimiter()
    {
        //arrange 
        var client = _factory.CreateClient();

        //action
        UserLoginDTO loginDTO = new UserLoginDTO
        {
            Username = "testName",
            Userpassword = "testPass"
        };
        var response = await client.PostAsJsonAsync("/api/Auth/login", loginDTO);

        //assert

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        
    }
}