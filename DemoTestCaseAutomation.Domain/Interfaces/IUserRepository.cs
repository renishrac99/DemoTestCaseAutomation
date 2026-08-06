using DemoTestCaseAutomation.Domain.Entities;

namespace DemoTestCaseAutomation.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> UpsertAsync(User user);
    Task<int> GetCountAsync();
}
