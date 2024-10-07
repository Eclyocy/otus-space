using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.WebAPI.Models;

namespace SpaceShip.WebAPI.Controllers;

/// <summary>
/// Публичный API для работы с сущностями типа "Корабль"
/// </summary>
[ApiController]
[Route("api/v1/spaceships")]
public class SpaceShipController : ControllerBase
{
    #region private fields

    private readonly IShipService _spaceShipService;
    private readonly IMapper _mapper;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор, в качестве параметра передаем сервис для работы с сущностью корабля
    /// </summary>
    /// <param name="spaceShipService">Сервис работы с сущностью корабль</param>
    /// <param name="mapper">Маппер ответа сервиса</param>
    public SpaceShipController(IShipService spaceShipService, IMapper mapper)
    {
        _spaceShipService = spaceShipService;
        _mapper = mapper;
    }

    #endregion

    #region endpoints

    /// <summary>
    /// Получение состояния корабля
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType<SpaceShipMetricResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult Get(Guid id)
    {
        ShipDTO spaceShipDTO = _spaceShipService.GetShip(id);
        SpaceShipMetricResponse response = _mapper.Map<SpaceShipMetricResponse>(spaceShipDTO);
        return Results.Ok(response);
    }

    /// <summary>
    /// Создать новый корабль
    /// </summary>
    /// <returns>201</returns>
    [HttpPost(Name = "Создать новый корабль")]
    [Produces("application/json")]
    [ProducesResponseType<SpaceShipCreateResponse>(StatusCodes.Status201Created)]
    public IResult Create()
    {
        ShipDTO spaceShipDTO = _spaceShipService.CreateShip();
        SpaceShipCreateResponse response = _mapper.Map<SpaceShipCreateResponse>(spaceShipDTO);
        return Results.Created("api/v1/spaceship", response);
    }

    /// <summary>
    /// Обновить существующий корабль
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>200</returns>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [Obsolete]
    public IResult Edit(Guid id) => Results.Ok();

    /// <summary>
    /// Удалить корабль по id
    /// </summary>
    /// <param name="id">ID корабля</param>
    /// <returns>200</returns>
    [HttpDelete("{id}")]
    [Obsolete]
    public IResult Delete(Guid id) => Results.Ok();

    #endregion
}
