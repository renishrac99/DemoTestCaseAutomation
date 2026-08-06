using DemoTestCaseAutomation.Application.DTOs;

namespace DemoTestCaseAutomation.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> UpsertUserAsync(UserDto userDto);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}
