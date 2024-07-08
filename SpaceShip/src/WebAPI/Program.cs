using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockSpaceShip.Service;
using SpaceShip.Service.Contracts;
using SpaceShip.Services.Queue;
using SpaceShip.Service.Interfaces;
using SpaceShip.WebAPI.Controllers;
using SpaceShip.WebAPI.Models;
using SpaceShip.WebAPI.Settings;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System;
using SpaceShip.WebAPI.Mappers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(static options =>
    {
        options.SwaggerDoc(
            name: "v1",
            info: new()
            {
                Title = "Spaceship controller API",
                Version = "v1",
                Description = "Публичный API для работы c кораблем и его ресурсами"
            }
        );
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
builder.Services.AddControllers();

// SpaceShip services registration:
builder.Services.AddTransient<IShipService, MockSpaceShipService>();
builder.Services.AddHostedService<TroubleEventProvider>();
builder.Services.AddHostedService<StepEventProvider>();

// Automapper:
builder.Services.AddSingleton<IMapper>(
    new Mapper(new MapperConfiguration(
                    static cfg => cfg.AddProfile<SpaceShipMappingProfile>())));

// RabbitMQ:
// ToDo

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
    {
        _ = endpoints.MapControllers();
    });
app.Run();
