using System.ComponentModel.DataAnnotations;

namespace AresLife.Api.DTOs;

public class ResourceReadingCreateDto
{
    [Range(1, int.MaxValue)]
    public int HabitatId { get; set; }

    [Range(0, 100)]
    public decimal OxygenLevel { get; set; }

    [Range(0, 100)]
    public decimal WaterLevel { get; set; }

    [Range(0, 100)]
    public decimal EnergyLevel { get; set; }

    [Range(-100, 100)]
    public decimal Temperature { get; set; }
}