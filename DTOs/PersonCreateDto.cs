using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.DTOs;

public class PersonCreateDto
{
    [Required]
    [MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string HealthStatus { get; set; } = "Stable";

    [Range(1, int.MaxValue)]
    public int HabitatId { get; set; }
}