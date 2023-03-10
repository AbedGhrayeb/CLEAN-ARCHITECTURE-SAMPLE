using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .AddPresintation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
var app = builder.Build();
app.UseExceptionHandler("/errors");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
