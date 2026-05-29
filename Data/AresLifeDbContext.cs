using AresLife.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AresLife.Api.Data;

public class AresLifeDbContext : DbContext
{
    public AresLifeDbContext(DbContextOptions<AresLifeDbContext> options) : base(options)
    {
    }

    public DbSet<Habitat> Habitats => Set<Habitat>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<ResourceReading> ResourceReadings => Set<ResourceReading>();
    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habitat>()
            .HasMany(h => h.People)
            .WithOne(p => p.Habitat)
            .HasForeignKey(p => p.HabitatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Habitat>()
            .HasMany(h => h.ResourceReadings)
            .WithOne(r => r.Habitat)
            .HasForeignKey(r => r.HabitatId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Habitat>()
            .HasMany(h => h.Alerts)
            .WithOne(a => a.Habitat)
            .HasForeignKey(a => a.HabitatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}