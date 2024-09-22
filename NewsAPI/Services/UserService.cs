using NewsAPI.Models;
using Microsoft.AspNetCore.Identity;
using NewsAPI.Shared;
using NewsAPI.Services.Interfaces;
using NewsAPI.DTOs;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> RegisterUserAsync(UserRegistrationDTO registrationDto)
    {
        var user = new User
        {
            UserName = registrationDto.Email,
            Email = registrationDto.Email,
            Role = registrationDto.Role
        };

        var result = await _userManager.CreateAsync(user, registrationDto.Password);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        return Result.Failure("Registration failed.");
    }

    public async Task<Result> LoginUserAsync(UserLoginDTO loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Result.Failure("Login failed.");
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
