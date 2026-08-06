using DemoTestCaseAutomation.Application.Services;
using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Services;

public class DashboardServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMasterRepository> _masterRepositoryMock;
    private readonly DashboardService _dashboardService;

    public DashboardServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _masterRepositoryMock = new Mock<IMasterRepository>();
        _dashboardService = new DashboardService(_userRepositoryMock.Object, _masterRepositoryMock.Object);
    }

    [Fact]
    public async Task GetDashboardStatsAsync_ReturnsDashboardStatsDto()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetCountAsync()).ReturnsAsync(10);
        
        var states = new List<State> { new State { Id = 1, Name = "State 1" } };
        _masterRepositoryMock.Setup(repo => repo.GetStatesAsync()).ReturnsAsync(states);

        var cities = new List<City> { new City { Id = 1, Name = "City 1", StateId = 1 } };
        _masterRepositoryMock.Setup(repo => repo.GetCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await _dashboardService.GetDashboardStatsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.TotalUserCount);
        Assert.Single(result.StateNames);
        Assert.Equal("State 1", result.StateNames.First());
        Assert.Single(result.CityNames);
        Assert.Equal("City 1", result.CityNames.First());
    }
}
