using Bluegrass.Data.Authentication;
using Bluegrass.Shared.DTO.Auth;
using Microsoft.AspNetCore.Identity;

namespace Bluegrass.Core.Services.UserAuthenticationService
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegistrationDto userForRegistration);
        Task<IdentityResult> RegisterAdminAsync(UserRegistrationDto userfouserForRegistration);
        Task<bool> ValidateUserAsync(LoginDto loginDto);
        Task<string> CreateTokenAsync();
    }
}
