using AresLife.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly AresLifeDbContext _context;

    public AlertsController(AresLifeDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        var alerts = await _context.Alerts
            .Include(a => a.Habitat)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new
            {
                a.Id,
                a.HabitatId,
                HabitatName = a.Habitat != null ? a.Habitat.Name : null,
                a.Type,
                a.Message,
                a.Severity,
                a.Resolved,
                a.CreatedAt
            })
            .ToListAsync();

        return alerts;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetById(int id)
    {
        var alert = await _context.Alerts
            .Include(a => a.Habitat)
            .Where(a => a.Id == id)
            .Select(a => new
            {
                a.Id,
                a.HabitatId,
                HabitatName = a.Habitat != null ? a.Habitat.Name : null,
                a.Type,
                a.Message,
                a.Severity,
                a.Resolved,
                a.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (alert == null)
            return NotFound(new { message = "Alert not found." });

        return alert;
    }

    [HttpGet("habitat/{habitatId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetByHabitat(int habitatId)
    {
        var habitatExists = await _context.Habitats.AnyAsync(h => h.Id == habitatId);

        if (!habitatExists)
            return NotFound(new { message = "Habitat not found." });

        var alerts = await _context.Alerts
            .Where(a => a.HabitatId == habitatId)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new
            {
                a.Id,
                a.HabitatId,
                a.Type,
                a.Message,
                a.Severity,
                a.Resolved,
                a.CreatedAt
            })
            .ToListAsync();

        return alerts;
    }

    [HttpPut("{id}/resolve")]
    public async Task<IActionResult> Resolve(int id)
    {
        var alert = await _context.Alerts.FindAsync(id);

        if (alert == null)
            return NotFound(new { message = "Alert not found." });

        alert.Resolved = true;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}