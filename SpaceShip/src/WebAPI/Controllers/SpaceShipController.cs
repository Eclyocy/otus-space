
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SpaceshipServices;
using Spaceship.DataLayer.EfClasses;


namespace WebAPI.Controllers;

/// <summary>
/// Публичный API для работы с сущностями типа "Корабль"
/// </summary>
[ApiController]
[Route("api/v1/spaceship")]
public class SpaceShipController : ControllerBase
{
    private readonly ISpaceshipService _spaceshipService;

    public SpaceShipController(ISpaceshipService spaceshipService)
    {
        _spaceshipService = spaceshipService;
    }

    [HttpPost]
    [Route("SpaceshipAdd")]
    public SpaceshipDTO Create(SpaceshipDTO resourceDTO)
    {
        return _spaceshipService.Create(resourceDTO);
    }

    ///// <summary>
    ///// Контроллер для работы с кораблем
    ///// </summary>
    //#region private fields
    //public readonly IShipService _service;

    //#endregion

    ///// <summary>
    ///// Кнструктор, в качестве праметра передаем сервис для работы с сущностью корабля
    ///// </summary>
    ///// <param name="service"></param>
    //#region constructor

    //public SpaceShipController(IShipService service) => _service = service;

    //#endregion

    ///// <summary>
    ///// Получение структуры описания корабля по id 
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>

    //[HttpGet("{id}")]
    //public IResult Get(int id) => Results.Ok(id);

    ///// <summary>
    ///// Создать новый корабль
    ///// </summary>
    ///// <returns>201</returns>
    //[HttpPost]
    //[ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    //public IResult Create() 
    //{
    //    return Results.Created("api/v1/spaceship",_service.CreateShip());
    //}

    ///// <summary>
    ///// Обновить существующий корабль
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>

    //[HttpPut("{id}")]
    //public IResult Edit(int id) => Results.Ok();

    ///// <summary>
    ///// Удалить корабль по id
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>

    //[HttpDelete("{id}")]
    //public IResult Delete(int id) => Results.Ok();
}
