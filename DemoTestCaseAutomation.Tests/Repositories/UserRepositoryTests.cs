using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Infrastructure.Data;
using DemoTestCaseAutomation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Repositories;

public class UserRepositoryTests
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
    public async Task GetAllAsync_ReturnsAllUsers()
    {
        // Arrange
        using var context = GetDbContext();
        context.Users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "j@d.com", CityId = 1 });
        await context.SaveChangesAsync();
        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser_WhenUserExists()
    {
        // Arrange
        using var context = GetDbContext();
        context.Users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "j@d.com", CityId = 1 });
        await context.SaveChangesAsync();
        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetCountAsync_ReturnsCount()
    {
        // Arrange
        using var context = GetDbContext();
        context.Users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "j@d.com", CityId = 1 });
        await context.SaveChangesAsync();
        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetCountAsync();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpsertAsync_AddsNewUser_WhenUserDoesNotExist()
    {
        // Arrange
        using var context = GetDbContext();
        var repo = new UserRepository(context);
        var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "j@d.com", CityId = 1 };

        // Act
        var result = await repo.UpsertAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, context.Users.Count());
    }

    [Fact]
    public async Task UpsertAsync_UpdatesUser_WhenUserExists()
    {
        // Arrange
        using var context = GetDbContext();
        context.Users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "j@d.com", CityId = 1 });
        await context.SaveChangesAsync();
        var repo = new UserRepository(context);
        var user = new User { Id = 1, FirstName = "Jane", LastName = "Doe", Email = "jane@d.com", CityId = 1 };

        // Act
        var result = await repo.UpsertAsync(user);

        // Assert
        Assert.NotNull(result);
        var dbUser = await context.Users.FindAsync(1);
        Assert.Equal("Jane", dbUser!.FirstName);
    }
}
