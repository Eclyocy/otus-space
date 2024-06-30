using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceShip.Service;
using SpaceShip.Services.Queue;
using WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(
            name: "v1",
            info: new() { Title = "Spaceship controller API", Version = "v1" }
        );
    });
builder.Services.AddControllers();
builder.Services.AddTransient<IShipService,MockSpaceShipService>();
builder.Services.AddHostedService<TroubleEventProvider>();
builder.Services.AddHostedService<StepEventProvider>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => 
    {
        _ = endpoints.MapControllers();
    });
app.Run();
