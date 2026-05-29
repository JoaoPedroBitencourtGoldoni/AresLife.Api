using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.DTOs;

public class HabitatCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string Location { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Capacity { get; set; }

    [Required]
    [MaxLength(30)]
    public string Status { get; set; } = "Active";
}