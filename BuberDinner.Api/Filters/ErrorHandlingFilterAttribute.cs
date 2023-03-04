using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context == null) { return; }
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request!",
                Status = (int)HttpStatusCode.InternalServerError,
                Type="Http://tools.ietf.org/html/rfc7231#section-6.6.1",
            };
            context.Result = new ObjectResult(problemDetails);

            context.ExceptionHandled=true;
        }
    }
}
