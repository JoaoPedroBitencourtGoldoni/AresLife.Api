using AresLife.Api.Data;
using AresLife.Api.DTOs;
using AresLife.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AresLifeDbContext _context;

    public PeopleController(AresLifeDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAll()
    {
        return await _context.People
            .Include(p => p.Habitat)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetById(int id)
    {
        var person = await _context.People
            .Include(p => p.Habitat)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
            return NotFound(new { message = "Person not found." });

        return person;
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Create(PersonCreateDto dto)
    {
        var habitatExists = await _context.Habitats.AnyAsync(h => h.Id == dto.HabitatId);

        if (!habitatExists)
            return BadRequest(new { message = "Invalid habitat id." });

        var person = new Person
        {
            FullName = dto.FullName,
            Role = dto.Role,
            HealthStatus = dto.HealthStatus,
            HabitatId = dto.HabitatId
        };

        _context.People.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PersonCreateDto dto)
    {
        var person = await _context.People.FindAsync(id);

        if (person == null)
            return NotFound(new { message = "Person not found." });

        var habitatExists = await _context.Habitats.AnyAsync(h => h.Id == dto.HabitatId);

        if (!habitatExists)
            return BadRequest(new { message = "Invalid habitat id." });

        person.FullName = dto.FullName;
        person.Role = dto.Role;
        person.HealthStatus = dto.HealthStatus;
        person.HabitatId = dto.HabitatId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var person = await _context.People.FindAsync(id);

        if (person == null)
            return NotFound(new { message = "Person not found." });

        _context.People.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}