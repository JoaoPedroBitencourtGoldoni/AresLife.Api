using AresLife.Api.Models;

namespace AresLife.Api.Service;

public class AlertService
{
    public List<Alert> GenerateAlerts(ResourceReading reading)
    {
        var alerts = new List<Alert>();

        if (reading.OxygenLevel < 19)
        {
            alerts.Add(new Alert
            {
                HabitatId = reading.HabitatId,
                Type = "Oxygen",
                Severity = "Critical",
                Message = $"Critical oxygen level detected: {reading.OxygenLevel}%."
            });
        }

        if (reading.WaterLevel < 25)
        {
            alerts.Add(new Alert
            {
                HabitatId = reading.HabitatId,
                Type = "Water",
                Severity = "High",
                Message = $"Low water level detected: {reading.WaterLevel}%."
            });
        }

        if (reading.EnergyLevel < 30)
        {
            alerts.Add(new Alert
            {
                HabitatId = reading.HabitatId,
                Type = "Energy",
                Severity = "High",
                Message = $"Low energy level detected: {reading.EnergyLevel}%."
            });
        }

        if (reading.Temperature < -40 || reading.Temperature > 50)
        {
            alerts.Add(new Alert
            {
                HabitatId = reading.HabitatId,
                Type = "Temperature",
                Severity = "Critical",
                Message = $"Unsafe temperature detected: {reading.Temperature}°C."
            });
        }

        return alerts;
    }
}