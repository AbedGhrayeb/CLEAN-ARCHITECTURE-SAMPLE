using BuberDinner.Application.Authentication;
using BuberDinner.Contracts.Authintication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            return Ok(new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token));
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);
            return Ok(new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token));
        }
    }
}