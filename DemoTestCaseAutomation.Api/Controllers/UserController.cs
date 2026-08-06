using DemoTestCaseAutomation.Application.DTOs;
using DemoTestCaseAutomation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoTestCaseAutomation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Upsert(UserDto userDto)
    {
        var result = await _userService.UpsertUserAsync(userDto);
        return Ok(result);
    }
}
