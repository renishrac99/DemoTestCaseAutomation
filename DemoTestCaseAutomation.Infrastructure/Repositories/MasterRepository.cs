using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Domain.Interfaces;
using DemoTestCaseAutomation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoTestCaseAutomation.Infrastructure.Repositories;

public class MasterRepository : IMasterRepository
{
    private readonly AppDbContext _context;

    public MasterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.Include(c => c.State).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId)
    {
        if (stateId > 0)
        {
            return await _context.Cities.Where(z => z.StateId == stateId).ToListAsync();
        }
        return await _context.Cities.Include(c => c.State).Where(c => c.StateId == stateId).ToListAsync();
    }

    public async Task<IEnumerable<State>> GetStatesAsync()
    {
        return await _context.States.ToListAsync();
    }

    public async Task<IEnumerable<ZipCode>> GetZipCodesByCityIdAsync(int cityId)
    {
        if(cityId > 0) {
            return await _context.ZipCodes.Where(z => z.CityId == cityId).ToListAsync();
        }
        return Enumerable.Empty<ZipCode>();
    }

    public async Task<IEnumerable<Domain.Entities.TimeZone>> GetTimeZonesAsync()
    {
        return await _context.TimeZones.ToListAsync();
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }
}
