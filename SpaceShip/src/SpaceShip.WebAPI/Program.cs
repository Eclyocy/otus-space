using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Mappers;
using SpaceShip.Service.Implementation;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Mappers;
using SpaceShip.Service.Queue;
using SpaceShip.Service.Services;
using SpaceShip.WebAPI.Mappers;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddTransient<IProblemService, ProblemService>();
builder.Services.AddTransient<IResourceService, ResourceService>();
builder.Services.AddTransient<IResourceTypeService, ResourceTypeService>();
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
                cfg.AddProfile<ProblemModelMappingProfile>();
                cfg.AddProfile<ResourceModelMappingProfile>();
                cfg.AddProfile<ResourceStateModelMappingProfile>();
                cfg.AddProfile<ResourceTypeModelMappingProfile>();
            })));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDatabase();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseAuthorization();

app.MapControllers();

app.Run();
