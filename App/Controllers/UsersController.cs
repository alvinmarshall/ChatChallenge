using App.DTO;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<object>>> Registration([FromBody] CreateUserDto input)
    {
        var apiResponse = new ApiResponse<object>()
        {
            Success = true,
            Data = await _userService.register(input)
        };
        return Ok(apiResponse);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<object>>> Login([FromBody] LoginDto input)
    {
        var apiResponse = new ApiResponse<object>
        {
            Success = true,
            Data = await _userService.Login(input)
        };
        return Ok(apiResponse);
    }
}