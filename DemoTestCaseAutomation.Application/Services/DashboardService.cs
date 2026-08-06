using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using DemoTestCaseAutomation.Domain.Interfaces;

namespace DemoTestCaseAutomation.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly IMasterRepository _masterRepository;

    public DashboardService(IUserRepository userRepository, IMasterRepository masterRepository)
    {
        _userRepository = userRepository;
        _masterRepository = masterRepository;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        var userCount = await _userRepository.GetCountAsync();
        var states = await _masterRepository.GetStatesAsync();
        var cities = await _masterRepository.GetCitiesAsync();

        return new DashboardStatsDto
        {
            TotalUserCount = userCount,
            StateNames = states.Select(s => s.Name),
            CityNames = cities.Select(c => c.Name)
        };
    }
}
