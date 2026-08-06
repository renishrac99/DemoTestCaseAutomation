using DemoTestCaseAutomation.Application.Services;
using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Services;

public class MasterServiceTests
{
    private readonly Mock<IMasterRepository> _masterRepositoryMock;
    private readonly MasterService _masterService;

    public MasterServiceTests()
    {
        _masterRepositoryMock = new Mock<IMasterRepository>();
        _masterService = new MasterService(_masterRepositoryMock.Object);
    }

    [Fact]
    public async Task GetCitiesAsync_ReturnsCityDtos()
    {
        // Arrange
        var cities = new List<City>
        {
            new City { Id = 1, Name = "City 1", StateId = 1 },
            new City { Id = 2, Name = "City 2", StateId = 1 }
        };
        _masterRepositoryMock.Setup(repo => repo.GetCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await _masterService.GetCitiesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCitiesByStateIdAsync_ReturnsCityDtos()
    {
        // Arrange
        var cities = new List<City>
        {
            new City { Id = 1, Name = "City 1", StateId = 1 }
        };
        _masterRepositoryMock.Setup(repo => repo.GetCitiesByStateIdAsync(1)).ReturnsAsync(cities);

        // Act
        var result = await _masterService.GetCitiesByStateIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("City 1", result.First().Name);
    }

    [Fact]
    public async Task GetStatesAsync_ReturnsStateDtos()
    {
        // Arrange
        var states = new List<State>
        {
            new State { Id = 1, Name = "State 1" }
        };
        _masterRepositoryMock.Setup(repo => repo.GetStatesAsync()).ReturnsAsync(states);

        // Act
        var result = await _masterService.GetStatesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
