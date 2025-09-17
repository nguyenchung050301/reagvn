
using System.Text.Json;
using StackExchange.Redis;

namespace e_commercial.Services;

public static class CacheExtension
{
    //Viet them extension
    public static async Task MyStringSetAsync<T>(this IDatabase databaseRedis, string key, T value, int second)
    {
        var json = JsonSerializer.Serialize(value);
        var setTask = databaseRedis.StringSetAsync(key, json);
        var expireTask = databaseRedis.KeyExpireAsync(key, TimeSpan.FromSeconds(second));
        await Task.WhenAll(setTask, expireTask);
    }
}