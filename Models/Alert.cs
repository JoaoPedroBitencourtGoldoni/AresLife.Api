using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.Models;

public class Alert
{
    public int Id { get; set; }

    public int HabitatId { get; set; }
    public Habitat? Habitat { get; set; }

    [Required]
    [MaxLength(40)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Severity { get; set; } = string.Empty;

    public bool Resolved { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}