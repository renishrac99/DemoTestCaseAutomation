using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using DemoTestCaseAutomation.Domain.Interfaces;

namespace DemoTestCaseAutomation.Application.Services;

public class MasterService : IMasterService
{
    private readonly IMasterRepository _masterRepository;

    public MasterService(IMasterRepository masterRepository)
    {
        _masterRepository = masterRepository;
    }

    public async Task<IEnumerable<CityDto>> GetCitiesAsync()
    {
        var cities = await _masterRepository.GetCitiesAsync();
        return cities.Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.Name,
            StateId = c.StateId
        });
    }

    public async Task<IEnumerable<CityDto>> GetCitiesByStateIdAsync(int stateId)
    {
        var cities = await _masterRepository.GetCitiesByStateIdAsync(stateId);
        return cities.Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.Name,
            StateId = c.StateId
        });
    }

    public async Task<IEnumerable<StateDto>> GetStatesAsync()
    {
        var states = await _masterRepository.GetStatesAsync();
        return states.Select(s => new StateDto
        {
            Id = s.Id,
            Name = s.Name
        });
    }

    public async Task<IEnumerable<ZipCodeDto>> GetZipCodesByCityIdAsync(int cityId)
    {
        var zipCodes = await _masterRepository.GetZipCodesByCityIdAsync(cityId);
        return zipCodes.Select(z => new ZipCodeDto
        {
            Id = z.Id,
            Code = z.Code,
            CityId = z.CityId
        });
    }

    public async Task<IEnumerable<TimeZoneDto>> GetTimeZonesAsync()
    {
        var timeZones = await _masterRepository.GetTimeZonesAsync();
        return timeZones.Select(t => new TimeZoneDto
        {
            Id = t.Id,
            Name = t.Name,
            Offset = t.Offset
        });
    }
}
