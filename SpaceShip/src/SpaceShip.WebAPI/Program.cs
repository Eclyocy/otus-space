﻿using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Mappers;
using SpaceShip.Notifications;
using SpaceShip.Service.Implementation;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Mappers;
using SpaceShip.Service.Queue;
using SpaceShip.WebAPI.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

#pragma warning disable SA1118 // ParameterMustNotSpanMultipleLines
builder.Services.AddSwaggerGen(static options =>
    {
        options.SwaggerDoc(
            name: "v1",
            info: new ()
            {
                Title = "Spaceship controller API",
                Version = "v1",
                Description = "Публичный API для работы c кораблем и его ресурсами",
            });
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
#pragma warning restore SA1118 // ParameterMustNotSpanMultipleLines

builder.Services.AddControllers().AddNewtonsoftJson(static options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy(),
    });
});

// SpaceShip services registration:
builder.Services.AddHostedService<TroubleEventConsumer>();
builder.Services.AddHostedService<StepEventConsumer>();
builder.Services.AddTransient<IShipService, SpaceShipService>();
builder.Services.AddScoped<IGameStepService, GameStepService>();

// Automapper:
builder.Services.AddSingleton<IMapper>(
    new Mapper(
        new MapperConfiguration(
            static cfg =>
            {
                cfg.AddProfile<SpaceShipMappingProfile>();
                cfg.AddProfile<SpaceShipModelMappingProfile>();
                cfg.AddProfile<ResourceModelMappingProfile>();
                cfg.AddProfile<ResourceStateModelMappingProfile>();
            })));

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNotifications();
builder.Services.ConfigureDatabase();

var app = builder.Build();

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
