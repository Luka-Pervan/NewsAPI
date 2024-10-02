using NewsAPI.Models;
using Microsoft.AspNetCore.Identity;
using NewsAPI.Shared;
using NewsAPI.Services.Interfaces;
using NewsAPI.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<Result> RegisterUserAsync(UserRegistrationDTO registrationDto)
    {
        var user = new User
        {
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            UserName = registrationDto.UserName,
            Email = registrationDto.Email,
            Role = registrationDto.Role
        };

        var result = await _userManager.CreateAsync(user, registrationDto.Password);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        return Result.Failure($"Registration failed. {string.Join(", ", result.Errors.Select(item => item.Code + "-" + item.Description))}");
    }

    public async Task<LoginResult> LoginUserAsync(UserLoginDTO loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            var token = GenerateJwtToken(user);
            return LoginResult.Success(token.TokenString, token.ExpDate);
        }
        return LoginResult.Failure("Invalid login attempt.");
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    #region Methods
    private Token GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        claims.Add(new Claim(ClaimTypes.Role, user.Role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );
        Token resultToken = new Token();
        resultToken.TokenString = new JwtSecurityTokenHandler().WriteToken(token);
        resultToken.ExpDate = DateTime.Now.AddMinutes(30);
        return resultToken;
    }

    #endregion

}
