using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;
using DemoTestCaseAutomation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoTestCaseAutomation.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.Include(u => u.City).ThenInclude(c => c.State).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.Include(u => u.City).ThenInclude(c => c.State).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<User> UpsertAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser == null)
        {
            _context.Users.Add(user);
        }
        else
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.CityId = user.CityId;
        }

        await _context.SaveChangesAsync();
        return user;
    }
}
