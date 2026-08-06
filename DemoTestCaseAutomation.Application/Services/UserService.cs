using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;

namespace DemoTestCaseAutomation.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            CityId = u.CityId
        });
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CityId = user.CityId
        };
    }

    public async Task<UserDto> UpsertUserAsync(UserDto userDto)
    {
        var user = new User
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            CityId = userDto.CityId
        };

        var result = await _userRepository.UpsertAsync(user);

        return new UserDto
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            CityId = result.CityId
        };
    }
}
