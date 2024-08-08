using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Implementation;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Mappers;
using SpaceShip.Service.Implementation;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Mappers;
using SpaceShip.Service.Queue;
using SpaceShip.Service.Services;
using SpaceShip.WebAPI.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfCoreContext>();

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
builder.Services
.AddHostedService<TroubleEventConsumer>()
.AddHostedService<StepEventConsumer>()
.AddTransient<IProblemRepository, ProblemRepository>()
.AddTransient<IResourceRepository, ResourceRepository>()
.AddTransient<IResourceTypeRepository, ResourceTypeRepository>()
.AddTransient<ISpaceshipRepository, SpaceshipRepository>()
.AddTransient<IProblemService, ProblemService>()
.AddTransient<IResourceService, ResourceService>()
.AddTransient<IResourceTypeService, ResourceTypeService>()
.AddTransient<IShipService, SpaceShipService>();

// Automapper:
builder.Services.AddSingleton<IMapper>(
    new Mapper(
        new MapperConfiguration(
            static cfg =>
            {
                cfg.AddProfile<SpaceShipMappingProfile>();
                cfg.AddProfile<SpaceShipModelMappingProfile>();
                cfg.AddProfile<ProblemModelMappingProfile>();
            })));

// RabbitMQ --> TODO
//
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
