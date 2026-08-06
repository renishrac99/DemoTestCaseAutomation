using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Services;
using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;
using Moq;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsUserDtos()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", CityId = 1 },
            new User { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", CityId = 2 }
        };
        _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("John", result.First().FirstName);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUserDto_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", CityId = 1 };
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetUserByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpsertUserAsync_ReturnsUserDto()
    {
        // Arrange
        var userDto = new UserDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", CityId = 1 };
        var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", CityId = 1 };
        
        _userRepositoryMock.Setup(repo => repo.UpsertAsync(It.IsAny<User>())).ReturnsAsync(user);

        // Act
        var result = await _userService.UpsertUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John", result.FirstName);
    }
}
