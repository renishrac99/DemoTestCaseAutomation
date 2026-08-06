using DemoTestCaseAutomation.Domain.Entities;

namespace DemoTestCaseAutomation.Domain.Interfaces;

public interface IMasterRepository
{
    Task<IEnumerable<State>> GetStatesAsync();
    Task<IEnumerable<City>> GetCitiesAsync();
    Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId);
}
