using System.Text.Json;
using RedisApi.Models;
using StackExchange.Redis;

namespace RedisApi.Data;

public class RedisPlatform: IPlatform
{
    private readonly IConnectionMultiplexer _multiplexer;
    public RedisPlatform(IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
    }
    
    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentOutOfRangeException(nameof(platform));
        }

        var db = _multiplexer.GetDatabase();
        var serialPlat = JsonSerializer.Serialize(platform);

        db.HashSet("hashplatform", new HashEntry[]
        {
            new HashEntry(platform.Id, serialPlat)
        });
    }

    public Platform? GetPlatformById(string id)
    {
        var db = _multiplexer.GetDatabase();


        var platform = db.HashGet("hashplatform", id);

        if (!string.IsNullOrEmpty(platform))
        {
            return JsonSerializer.Deserialize<Platform>(platform);
        }

        return null;
    }

    public List<Platform?> GetAllPlatforms()
    {
        var db = _multiplexer.GetDatabase();

        var completeHesh = db.HashGetAll("hashplatform");

        if (completeHesh.Length > 0)
        {
            List<Platform?> obj = Array.ConvertAll(completeHesh, val =>
                JsonSerializer.Deserialize<Platform>(val.Value)).ToList();

            return obj;
        }

        return null;
    }
}