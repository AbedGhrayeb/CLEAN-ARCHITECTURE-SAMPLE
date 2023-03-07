using BuberDinner.Api.Common.Http;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            if (errors == null || !errors.Any())
                return Problem();

            HttpContext.Items[HttpContextItemKey.Errors] = errors;

            if (errors.All(x => x.Type == ErrorType.Validation))
                return ValidationProblem(errors);
            var firsError = errors[0];
            return Problem(firsError);
        }

        private IActionResult Problem(Error firsError)
        {
            var statusCode = firsError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError,

            };
            return Problem(statusCode: statusCode, title: firsError.Description);
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
                modelStateDictionary.AddModelError(error.Code, error.Description);
            return ValidationProblem(modelStateDictionary);
        }
    }
}
