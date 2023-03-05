using BuberDinner.Api.Common.Http;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected IActionResult Problem(List<Error> errors)
        {
            HttpContext.Items[HttpContextItemKey.Errors] = errors;
            var firsError = errors[0];
            var statusCode = firsError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,

            };
            return Problem(statusCode: statusCode, title: firsError.Description);
        }
    }
}
