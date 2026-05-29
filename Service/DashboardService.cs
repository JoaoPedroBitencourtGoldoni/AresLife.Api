using AresLife.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Service;

public class DashboardService
{
    private readonly AresLifeDbContext _context;

    public DashboardService(AresLifeDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetDashboardAsync()
    {
        var totalHabitats = await _context.Habitats.CountAsync();
        var totalPeople = await _context.People.CountAsync();
        var totalReadings = await _context.ResourceReadings.CountAsync();
        var totalAlerts = await _context.Alerts.CountAsync();
        var criticalAlerts = await _context.Alerts.CountAsync(a => a.Severity == "Critical" && !a.Resolved);

        return new
        {
            totalHabitats,
            totalPeople,
            totalReadings,
            totalAlerts,
            criticalAlerts,
            status = criticalAlerts > 0 ? "Attention required" : "Operational"
        };
    }
}