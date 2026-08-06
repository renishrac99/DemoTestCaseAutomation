using DemoTestCaseAutomation.Infrastructure.Data;
using DemoTestCaseAutomation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Repositories;

public class MasterRepositoryTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task GetCitiesAsync_ReturnsAllCities()
    {
        // Arrange
        using var context = GetDbContext();
        var repo = new MasterRepository(context);

        // Act
        var result = await repo.GetCitiesAsync();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetCitiesByStateIdAsync_ReturnsCitiesForState()
    {
        // Arrange
        using var context = GetDbContext();
        var repo = new MasterRepository(context);

        // Act
        var result = await repo.GetCitiesByStateIdAsync(1);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, c => Assert.Equal(1, c.StateId));
    }

    [Fact]
    public async Task GetStatesAsync_ReturnsAllStates()
    {
        // Arrange
        using var context = GetDbContext();
        var repo = new MasterRepository(context);

        // Act
        var result = await repo.GetStatesAsync();

        // Assert
        Assert.NotEmpty(result);
    }
}
