using AresLife.Api.Data;
using AresLife.Api.DTOs;
using AresLife.Api.Models;
using AresLife.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Controllers;

[ApiController]
[Route("api/resource-readings")]
public class ResourceReadingsController : ControllerBase
{
    private readonly AresLifeDbContext _context;
    private readonly AlertService _alertService;

    public ResourceReadingsController(AresLifeDbContext context, AlertService alertService)
    {
        _context = context;
        _alertService = alertService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceReading>>> GetAll()
    {
        return await _context.ResourceReadings
            .Include(r => r.Habitat)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceReading>> GetById(int id)
    {
        var reading = await _context.ResourceReadings
            .Include(r => r.Habitat)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reading == null)
            return NotFound(new { message = "Resource reading not found." });

        return reading;
    }

    [HttpGet("habitat/{habitatId}")]
    public async Task<ActionResult<IEnumerable<ResourceReading>>> GetByHabitat(int habitatId)
    {
        var habitatExists = await _context.Habitats.AnyAsync(h => h.Id == habitatId);

        if (!habitatExists)
            return NotFound(new { message = "Habitat not found." });

        return await _context.ResourceReadings
            .Where(r => r.HabitatId == habitatId)
            .OrderByDescending(r => r.ReadingDate)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(ResourceReadingCreateDto dto)
    {
        var habitatExists = await _context.Habitats.AnyAsync(h => h.Id == dto.HabitatId);

        if (!habitatExists)
            return BadRequest(new { message = "Invalid habitat id." });

        var reading = new ResourceReading
        {
            HabitatId = dto.HabitatId,
            OxygenLevel = dto.OxygenLevel,
            WaterLevel = dto.WaterLevel,
            EnergyLevel = dto.EnergyLevel,
            Temperature = dto.Temperature
        };

        _context.ResourceReadings.Add(reading);

        var generatedAlerts = _alertService.GenerateAlerts(reading);

        if (generatedAlerts.Any())
            _context.Alerts.AddRange(generatedAlerts);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = reading.Id }, new
        {
            reading,
            generatedAlerts
        });
    }
}