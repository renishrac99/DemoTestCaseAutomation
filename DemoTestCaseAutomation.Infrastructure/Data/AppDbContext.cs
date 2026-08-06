using DemoTestCaseAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DemoTestCaseAutomation.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<State> States { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed some master data
        modelBuilder.Entity<State>().HasData(
            new State { Id = 1, Name = "California" },
            new State { Id = 2, Name = "Texas" }
        );

        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "Los Angeles", StateId = 1 },
            new City { Id = 2, Name = "San Francisco", StateId = 1 },
            new City { Id = 3, Name = "Houston", StateId = 2 },
            new City { Id = 4, Name = "Austin", StateId = 2 }
        );
    }
}
