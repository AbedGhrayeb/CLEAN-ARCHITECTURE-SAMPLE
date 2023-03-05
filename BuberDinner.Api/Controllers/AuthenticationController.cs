using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authintication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await Mediator.Send(new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password));
            var response = authResult.MatchFirst(authResult =>
                Ok(MatchAuthResult(authResult)),
                firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));

            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await Mediator.Send(new LoginQuery(request.Email, request.Password));
            if (authResult.IsError && authResult.FirstError == Errors.User.InvalidCredential)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

            return authResult.Match(authResult => Ok(MatchAuthResult(authResult)), errors => Problem(errors));

        }

        private static AuthenticationResponse MatchAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
        }
    }
}