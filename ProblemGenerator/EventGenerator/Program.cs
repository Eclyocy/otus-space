using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EventGenerator.API.Helpers;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Services;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Repository;

//using EventGenerator.Services.Mappers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(x => x.AddProfile(typeof(EventMapper)));
// Add services to the container.
builder.Services.AddTransient<IGeneratorService, GeneratorService>();
builder.Services.AddTransient<IEventRepository, EventRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
