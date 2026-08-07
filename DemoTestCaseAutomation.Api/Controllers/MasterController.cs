using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoTestCaseAutomation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MasterController : ControllerBase
{
    private readonly IMasterService _masterService;

    public MasterController(IMasterService masterService)
    {
        _masterService = masterService;
    }

    [HttpGet("states")]
    public async Task<ActionResult<IEnumerable<StateDto>>> GetStates()
    {
        var states = await _masterService.GetStatesAsync();
        return Ok(states);
    }

    [HttpGet("cities")]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
    {
        var cities = await _masterService.GetCitiesAsync();
        return Ok(cities);
    }

    [HttpGet("cities/bystate/{stateId}")]
    public async Task<ActionResult<IEnumerable<CityDto>>> GetCitiesByState(int stateId)
    {
        var cities = await _masterService.GetCitiesByStateIdAsync(stateId);
        return Ok(cities);
    }
    [HttpGet("zipcodes/bycity/{cityId}")]
    public async Task<ActionResult<IEnumerable<ZipCodeDto>>> GetZipCodesByCity(int cityId)
    {
        var zipCodes = await _masterService.GetZipCodesByCityIdAsync(cityId);
        return Ok(zipCodes);
    }
}
