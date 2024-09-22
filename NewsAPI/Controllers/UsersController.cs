using Microsoft.AspNetCore.Mvc;
using NewsAPI.DTOs;
using NewsAPI.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registrationDto)
    {
        var result = await _userService.RegisterUserAsync(registrationDto);
        if (result.Succeeded)
        {
            return Ok("User registered successfully.");
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
    {
        var result = await _userService.LoginUserAsync(loginDto);
        if (result.Succeeded)
        {
            return Ok("User logged in successfully.");
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutUserAsync();
        return Ok("User logged out successfully.");
    }
}
