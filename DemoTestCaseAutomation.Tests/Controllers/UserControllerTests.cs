using DemoTestCaseAutomation.Api.Controllers;
using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _userController = new UserController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithUsers()
    {
        // Arrange
        var users = new List<UserDto>
        {
            new UserDto { Id = 1, FirstName = "John", LastName = "Doe" }
        };
        _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

        // Act
        var result = await _userController.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
        Assert.Single(returnedUsers);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var user = new UserDto { Id = 1, FirstName = "John", LastName = "Doe" };
        _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _userController.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(1, returnedUser.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((UserDto?)null);

        // Act
        var result = await _userController.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Upsert_ReturnsOkResult_WithUserDto()
    {
        // Arrange
        var userDto = new UserDto { Id = 1, FirstName = "John", LastName = "Doe" };
        _userServiceMock.Setup(s => s.UpsertUserAsync(userDto)).ReturnsAsync(userDto);

        // Act
        var result = await _userController.Upsert(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(1, returnedUser.Id);
    }
}
