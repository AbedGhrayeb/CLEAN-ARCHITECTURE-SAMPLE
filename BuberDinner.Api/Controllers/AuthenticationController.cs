using BuberDinner.Application.Authentication;
using BuberDinner.Contracts.Authintication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
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
            var response = authResult.MatchFirst(authResult =>
                Ok(MatchAuthResult(authResult)),
                firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));

            return Ok(response);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);
            if (authResult.IsError && authResult.FirstError == Errors.User.InvalidCredential)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

            return authResult.Match(authResult => Ok(MatchAuthResult(authResult)),errors => Problem(errors));

        }

        private static AuthenticationResponse MatchAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
        }
    }
}