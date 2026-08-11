using DemoTestCaseAutomation.Application.DTOs;

namespace DemoTestCaseAutomation.Application.Interfaces;

public interface IMasterService
{
    Task<IEnumerable<StateDto>> GetStatesAsync();
    Task<IEnumerable<CityDto>> GetCitiesAsync();
    Task<IEnumerable<CityDto>> GetCitiesByStateIdAsync(int stateId);
    Task<IEnumerable<ZipCodeDto>> GetZipCodesByCityIdAsync(int cityId);
    Task<IEnumerable<TimeZoneDto>> GetTimeZonesAsync();
    Task<IEnumerable<CountryDto>> GetCountriesAsync();
}
