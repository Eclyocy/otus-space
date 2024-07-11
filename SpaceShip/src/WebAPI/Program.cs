using DataLayer.Abstrations;
using DataLayer.EfCore;
using DataLayer.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer.ProblemServices;
using ServiceLayer.ProblemServices.Concrete;
using ServiceLayer.ResourceServices.Concrete;
using ServiceLayer.ResourceTypeServices;
using ServiceLayer.ResourceTypeServices.Concrete;
using ServiceLayer.SpaceshipServices;
using ServiceLayer.SpaceshipServices.Concrete;
using System.ComponentModel.Design;


var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение/ Это поле сервиса которое будет жить всегда когда наше прилождение запустилось
//в него можно накидывать разные полезные плюшки
builder.Services.AddDbContext<EfCoreContext>(options => options.UseNpgsql(connection));

builder.Services
    .AddTransient<IProblemRepository, ProblemRepository>()
    .AddTransient<IResourceRepository, ResourceRepository>()
    .AddTransient<IResourceTypeRepository, ResourceTypeRepository>()
    .AddTransient<ISpaceshipRepository, SpaceshipRepository>()
    .AddTransient<IProblemService, ProblemService>()
    .AddTransient<ServiceLayer.ResourceServices.IResourceService, ResourceService>()
    .AddTransient<IResourceTypeService, ResourceTypeService>()
    .AddTransient<ISpaceshipService, SpaceshipService>();
    


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//    {
//        options.SwaggerDoc(
//            name: "v1",
//            info: new() { Title = "Spaceship controller API", Version = "v1" }
//        );
//    });
//builder.Services.AddControllers();
//builder.Services.AddTransient<IShipService, MockSpaceShipService>();
//var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        options.RoutePrefix = string.Empty;
//    });

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//    {
//        _ = endpoints.MapControllers();
//    });
//app.MapControllers();
//app.Run();
