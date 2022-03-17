using RedisApi.Models;

namespace RedisApi.Data;

public interface IPlatform
{
    void CreatePlatform(Platform platform);
    Platform? GetPlatformById(string id);
    List<Platform?> GetAllPlatforms();
}