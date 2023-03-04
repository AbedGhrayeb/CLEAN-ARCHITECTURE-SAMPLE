using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(option=> option.Filters.Add<ErrorHandlingFilterAttribute>());
builder.Services.AddControllers();
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseExceptionHandler("/errors");
//app.Map("/error", (HttpContext context) =>
//{
//    Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//    var customProperty = new Dictionary<string, object?>();
//    return Results.Problem(title: exception?.Message,statusCode:500);
//});
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
