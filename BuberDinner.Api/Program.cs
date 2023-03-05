using BuberDinner.Api;
using BuberDinner.Api.Common.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
builder.Services.AddPresintation().AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseExceptionHandler("/errors");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
