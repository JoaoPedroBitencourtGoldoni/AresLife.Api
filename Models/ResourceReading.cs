using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.Models;

public class ResourceReading
{
    public int Id { get; set; }

    public int HabitatId { get; set; }
    public Habitat? Habitat { get; set; }

    [Range(0, 100)]
    public decimal OxygenLevel { get; set; }

    [Range(0, 100)]
    public decimal WaterLevel { get; set; }

    [Range(0, 100)]
    public decimal EnergyLevel { get; set; }

    [Range(-100, 100)]
    public decimal Temperature { get; set; }

    public DateTime ReadingDate { get; set; } = DateTime.UtcNow;
}