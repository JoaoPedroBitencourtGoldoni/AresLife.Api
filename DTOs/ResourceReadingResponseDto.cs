namespace AresLife.Api.DTOs;

public class ResourceReadingResponseDto
{
    public int Id { get; set; }
    public int HabitatId { get; set; }
    public string? HabitatName { get; set; }

    public decimal OxygenLevel { get; set; }
    public decimal WaterLevel { get; set; }
    public decimal EnergyLevel { get; set; }
    public decimal Temperature { get; set; }
    public DateTime ReadingDate { get; set; }
}