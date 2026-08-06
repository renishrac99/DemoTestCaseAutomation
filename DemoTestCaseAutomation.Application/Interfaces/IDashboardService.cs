using DemoTestCaseAutomation.Application.DTOs;

namespace DemoTestCaseAutomation.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}
