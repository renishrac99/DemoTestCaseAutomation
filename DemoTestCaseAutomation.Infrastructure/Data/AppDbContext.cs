using DemoTestCaseAutomation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TimeZone = DemoTestCaseAutomation.Domain.Entities.TimeZone;

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
    public DbSet<ZipCode> ZipCodes { get; set; } = null!;
    public DbSet<Domain.Entities.TimeZone> TimeZones { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;

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

        modelBuilder.Entity<ZipCode>().HasData(
            new ZipCode { Id = 1, Code = "90001", CityId = 1 },
            new ZipCode { Id = 2, Code = "94101", CityId = 2 },
            new ZipCode { Id = 3, Code = "77001", CityId = 3 },
            new ZipCode { Id = 4, Code = "73301", CityId = 4 }
        );

        modelBuilder.Entity<TimeZone>().HasData(
            new TimeZone { Id = 1, Name = "Pacific Standard Time", Offset = "-08:00" },
            new TimeZone { Id = 2, Name = "Central Standard Time", Offset = "-06:00" },
            new TimeZone { Id = 3, Name = "Eastern Standard Time", Offset = "-05:00" }
        );

        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "United States", Code = "US" },
            new Country { Id = 2, Name = "Canada", Code = "CA" },
            new Country { Id = 3, Name = "United Kingdom", Code = "UK" }
        );
    }
}
