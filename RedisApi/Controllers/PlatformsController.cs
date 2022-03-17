using Microsoft.AspNetCore.Mvc;
using RedisApi.Data;
using RedisApi.Models;

namespace RedisApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController: ControllerBase
{
    private readonly IPlatform _platform;
    public PlatformsController(IPlatform platform)
    {
        _platform = platform;
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var result = _platform.GetPlatformById(id);

        if (result == null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlatform(Platform platform)
    {
        _platform.CreatePlatform(platform);

        return CreatedAtRoute(nameof(GetPlatformById), new {Id = platform.Id}, platform);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
    {
        var result = _platform.GetAllPlatforms();

        return Ok(result);
    }
}