using DataLayer.EfCode;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaceShip.Service;


var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение/ Это поле сервиса которое будет жить всегда когда наше прилождение запустилось
//в него можно накидывать разные полезные плюшки
builder.Services.AddDbContext<EfCoreContext>(options => options.UseNpgsql(connection));

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
app.MapControllers();
app.Run();
