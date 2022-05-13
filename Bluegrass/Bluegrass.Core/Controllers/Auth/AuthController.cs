using Bluegrass.Core.Services.UserAuthenticationService;
using Bluegrass.Shared.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Members
        private readonly IUserAuthenticationService _userAuthenticationService;
        #endregion
        public AuthController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegistrationDto userRegistration)
        {
            var userResult = await _userAuthenticationService.RegisterAdminAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : Ok( new { StatusCode = 201 });
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            var userResult = await _userAuthenticationService.RegisterUserAsync(userRegistration);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : Ok( new { StatusCode = 201 });   // I wanted to use StatusCode(201), but the frontend had a issue reading it as an error
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto user)
        {
            return !await _userAuthenticationService.ValidateUserAsync(user) ? Unauthorized() : Ok(new { Token = await _userAuthenticationService.CreateTokenAsync()});
        }
        [HttpPost("validate-token")]
        public IActionResult ValidateToken(string token)
        {
            return !_userAuthenticationService.ValidateToken(token) ? new BadRequestObjectResult(new { TokenValid = false }) : Ok(new { TokenValid = true });
        }
    }
}
