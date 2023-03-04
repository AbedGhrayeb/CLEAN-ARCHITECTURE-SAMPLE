using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
