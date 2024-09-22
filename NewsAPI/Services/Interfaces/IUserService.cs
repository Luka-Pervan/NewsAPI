using NewsAPI.DTOs;
using NewsAPI.Shared;

namespace NewsAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterUserAsync(UserRegistrationDTO registrationDto);
        Task<Result> LoginUserAsync(UserLoginDTO loginDto);
        Task LogoutUserAsync();
    }
}
