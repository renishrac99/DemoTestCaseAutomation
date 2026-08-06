using DemoTestCaseAutomation.Api.Controllers;
using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Controllers;

public class DashboardControllerTests
{
    private readonly Mock<IDashboardService> _dashboardServiceMock;
    private readonly DashboardController _dashboardController;

    public DashboardControllerTests()
    {
        _dashboardServiceMock = new Mock<IDashboardService>();
        _dashboardController = new DashboardController(_dashboardServiceMock.Object);
    }

    [Fact]
    public async Task GetStats_ReturnsOkResult_WithStats()
    {
        // Arrange
        var stats = new DashboardStatsDto
        {
            TotalUserCount = 10,
            StateNames = new List<string> { "State 1" },
            CityNames = new List<string> { "City 1" }
        };
        _dashboardServiceMock.Setup(s => s.GetDashboardStatsAsync()).ReturnsAsync(stats);

        // Act
        var result = await _dashboardController.GetStats();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStats = Assert.IsType<DashboardStatsDto>(okResult.Value);
        Assert.Equal(10, returnedStats.TotalUserCount);
    }
}
