namespace AresLife.Api.DTOs;

public class AlertResponseDto
{
    public int Id { get; set; }
    public int HabitatId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool Resolved { get; set; }
    public DateTime CreatedAt { get; set; }
}