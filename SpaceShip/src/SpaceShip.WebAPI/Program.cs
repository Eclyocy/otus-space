using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MockSpaceShip.Service;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Queue;
using SpaceShip.WebAPI.Mappers;
using System;
using System.Reflection;

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

builder.Services.AddControllers().AddNewtonsoftJson(static options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    });
});

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
