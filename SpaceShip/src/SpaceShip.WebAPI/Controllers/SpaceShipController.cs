using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    private readonly IShipService _service;
    private readonly IMapper _mapper;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор, в качестве параметра передаем сервис для работы с сущностью корабля
    /// </summary>
    /// <param name="service">Сервис работы с сущностью корабль</param>
    /// <param name="mapper">Маппер ответа сервиса</param>
    public SpaceShipController(IShipService service, IMapper mapper)
    {
        _service = service;
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
        try
        {
            return Results.Ok(_mapper.Map<SpaceShipMetricResponse>(_service.Get(id)));
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound();
        }
    }

    /// <summary>
    /// Создать новый корабль
    /// </summary>
    /// <returns>200</returns>
    [HttpPost(Name = "Создать новый корабль")]
    [Produces("application/json")]
    [ProducesResponseType<SpaceShipCreateResponse>(StatusCodes.Status201Created)]
    public IResult Create()
    {
        return Results.Created("api/v1/spaceship", _mapper.Map<SpaceShipCreateResponse>(_service.CreateShip()));
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
