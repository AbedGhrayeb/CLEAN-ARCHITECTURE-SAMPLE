using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authintication;
using BuberDinner.Domain.Common.Errors;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await Mediator.Send(_mapper.Map<RegisterCommand>(request));
            var response = authResult.MatchFirst(authResult =>
                Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));

            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await Mediator.Send(_mapper.Map<LoginQuery>(request));
            if (authResult.IsError && authResult.FirstError == Errors.User.InvalidCredential)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

            return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)), errors => Problem(errors));

        }
    }
}