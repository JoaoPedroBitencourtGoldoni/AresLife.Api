using AresLife.Api.Data;
using AresLife.Api.DTOs;
using AresLife.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitatsController : ControllerBase
{
    private readonly AresLifeDbContext _context;

    public HabitatsController(AresLifeDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habitat>>> GetAll()
    {
        return await _context.Habitats
            .Include(h => h.People)
            .Include(h => h.ResourceReadings)
            .Include(h => h.Alerts)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Habitat>> GetById(int id)
    {
        var habitat = await _context.Habitats
            .Include(h => h.People)
            .Include(h => h.ResourceReadings)
            .Include(h => h.Alerts)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (habitat == null)
            return NotFound(new { message = "Habitat not found." });

        return habitat;
    }

    [HttpPost]
    public async Task<ActionResult<Habitat>> Create(HabitatCreateDto dto)
    {
        var habitat = new Habitat
        {
            Name = dto.Name,
            Location = dto.Location,
            Capacity = dto.Capacity,
            Status = dto.Status
        };

        _context.Habitats.Add(habitat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = habitat.Id }, habitat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, HabitatCreateDto dto)
    {
        var habitat = await _context.Habitats.FindAsync(id);

        if (habitat == null)
            return NotFound(new { message = "Habitat not found." });

        habitat.Name = dto.Name;
        habitat.Location = dto.Location;
        habitat.Capacity = dto.Capacity;
        habitat.Status = dto.Status;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var habitat = await _context.Habitats
            .Include(h => h.People)
            .FirstOrDefaultAsync(h => h.Id == id);

        if (habitat == null)
            return NotFound(new { message = "Habitat not found." });

        if (habitat.People.Any())
            return Conflict(new { message = "Cannot delete a habitat with registered people." });

        _context.Habitats.Remove(habitat);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}