using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.Models;

public class Habitat
{
    public int Id { get; set; }

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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Person> People { get; set; } = new();
    public List<ResourceReading> ResourceReadings { get; set; } = new();
    public List<Alert> Alerts { get; set; } = new();
}