using System.ComponentModel.DataAnnotations;

namespace RedisApi.Models;

public class Platform
{
    public string Id { get; set; } = $"platform:{Guid.NewGuid().ToString()}";
    
    [Required]
    public string? Name { get; set; }
}