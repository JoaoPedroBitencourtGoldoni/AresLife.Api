using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.Models;

public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string HealthStatus { get; set; } = "Stable";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int HabitatId { get; set; }
    public Habitat? Habitat { get; set; }
}