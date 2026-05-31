namespace AresLife.Api.DTOs;

public class PersonResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string HealthStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public int HabitatId { get; set; }
    public string? HabitatName { get; set; }
}