using e_commercial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace e_commercial.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
    private readonly IDatabase _redis;

    public HealthCheckController(IConnectionMultiplexer muxer)
    {
        _redis = muxer.GetDatabase();
    }

    [HttpGet]
    public async Task<IActionResult> GetString()
    {
        string value = "SASASASS";

        await _redis.MyStringSetAsync("keysample", value, 20);
        return Ok();
    }
}