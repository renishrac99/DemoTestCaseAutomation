using DemoTestCaseAutomation.Api.Controllers;
using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Controllers;

public class MasterControllerTests
{
    private readonly Mock<IMasterService> _masterServiceMock;
    private readonly MasterController _masterController;

    public MasterControllerTests()
    {
        _masterServiceMock = new Mock<IMasterService>();
        _masterController = new MasterController(_masterServiceMock.Object);
    }

    [Fact]
    public async Task GetStates_ReturnsOkResult_WithStates()
    {
        // Arrange
        var states = new List<StateDto>
        {
            new StateDto { Id = 1, Name = "State 1" }
        };
        _masterServiceMock.Setup(s => s.GetStatesAsync()).ReturnsAsync(states);

        // Act
        var result = await _masterController.GetStates();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStates = Assert.IsAssignableFrom<IEnumerable<StateDto>>(okResult.Value);
        Assert.Single(returnedStates);
    }

    [Fact]
    public async Task GetCities_ReturnsOkResult_WithCities()
    {
        // Arrange
        var cities = new List<CityDto>
        {
            new CityDto { Id = 1, Name = "City 1", StateId = 1 }
        };
        _masterServiceMock.Setup(s => s.GetCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await _masterController.GetCities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCities = Assert.IsAssignableFrom<IEnumerable<CityDto>>(okResult.Value);
        Assert.Single(returnedCities);
    }

    [Fact]
    public async Task GetCitiesByState_ReturnsOkResult_WithCities()
    {
        // Arrange
        var cities = new List<CityDto>
        {
            new CityDto { Id = 1, Name = "City 1", StateId = 1 }
        };
        _masterServiceMock.Setup(s => s.GetCitiesByStateIdAsync(1)).ReturnsAsync(cities);

        // Act
        var result = await _masterController.GetCitiesByState(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCities = Assert.IsAssignableFrom<IEnumerable<CityDto>>(okResult.Value);
        Assert.Single(returnedCities);
    }
}
