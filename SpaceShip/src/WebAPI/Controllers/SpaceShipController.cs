
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public static class SpaceShipController
{

    public static void ConfigureApi(this WebApplication app)
    {
        app.MapGet("/Ship/{id:int}", Get);
        app.MapPost("Ship", Create);
        app.MapPut("/Ship", Edit);
        app.MapDelete("/Ship", Delete);
    }

    public static IResult Get(int id) => Results.Ok();

    public static IResult Create() => Results.Ok();

    public static IResult Edit(int id) => Results.Ok();

    public static IResult Delete(int id) => Results.Ok();
}
