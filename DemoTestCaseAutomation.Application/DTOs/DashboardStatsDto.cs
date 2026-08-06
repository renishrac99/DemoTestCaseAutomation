using System.Collections.Generic;

namespace DemoTestCaseAutomation.Application.DTOs;

public class DashboardStatsDto
{
    public int TotalUserCount { get; set; }
    public IEnumerable<string> StateNames { get; set; } = new List<string>();
    public IEnumerable<string> CityNames { get; set; } = new List<string>();
}
