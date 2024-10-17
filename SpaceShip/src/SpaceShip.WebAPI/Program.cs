using System;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SpaceShip.Domain.ServiceCollectionExtensions;
using SpaceShip.Notifications;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Mappers;
using SpaceShip.Service.Queue;
using SpaceShip.Service.Services;
using SpaceShip.WebAPI.ApplicationBuilderExtensions;
using SpaceShip.WebAPI.LogFormatters;
using SpaceShip.WebAPI.Mappers;

#region application builder

var builder = WebApplication.CreateBuilder(args);

// Add custom logging
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole(options =>
{
    options.FormatterName = nameof(CustomLogFormatter);
});
builder.Logging.AddDebug();

builder.Services.AddSingleton<ConsoleFormatter, CustomLogFormatter>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(static options =>
{
    options.SwaggerDoc(
        name: "v1",
        info: new()
        {
            Title = "Spaceship controller API",
            Version = "v1",
            Description = "Публичный API для работы c кораблем и его ресурсами",
        });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// SpaceShip services registration:
builder.Services.AddHostedService<TroubleEventConsumer>();
builder.Services.AddHostedService<StepEventConsumer>();
builder.Services.AddTransient<IShipService, ShipService>();
builder.Services.AddTransient<IResourceService, ResourceService>();

// Automapper:
builder.Services.AddSingleton<IMapper>(
    new Mapper(
        new MapperConfiguration(
            static cfg =>
            {
                cfg.AddProfile<SpaceShipMappingProfile>();
                cfg.AddProfile<ShipMappingProfile>();
                cfg.AddProfile<ResourceMappingProfile>();
            })));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNotifications();
builder.Services.ConfigureDatabase();

#endregion

#region application

var app = builder.Build();

app.UseExceptionHandler(x => x.UseCustomErrorHandling());

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.UseNotifications();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.Run();

#endregion
