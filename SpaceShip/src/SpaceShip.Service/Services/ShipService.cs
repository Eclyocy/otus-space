﻿using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Enums;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;

namespace SpaceShip.Service.Services;

/// <summary>
/// Сервис для работы с сущностью "Корабль".
/// </summary>
public class ShipService : IShipService
{
    #region private fields

    private readonly IShipRepository _shipRepository;

    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ShipService(
        IShipRepository shipRepository,
        IMapper mapper,
        ILogger<ShipService> logger)
    {
        _shipRepository = shipRepository;

        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region public methods

    /// <summary>
    /// Создать новый корабль с ресурсами.
    /// </summary>
    /// <returns>ID корабля</returns>
    public ShipDTO CreateShip()
    {
        _logger.LogInformation("Create space ship");

        Ship ship = _shipRepository.Create(
            new()
            {
                Step = 0,
                State = ShipState.OK,
            },
            saveChanges: false);

        ship.Resources = new List<Resource>()
        {
            new Resource()
            {
                Name = "Обшивка",
                Amount = 1,
                State = ResourceState.OK,
                ResourceType = ResourceType.Hull
            },
            new Resource()
            {
                Name = "Металлический лом",
                Amount = 4,
                State = ResourceState.OK,
                ResourceType = ResourceType.ScrapMetal
            },
            new Resource()
            {
                Name = "Двигатель",
                Amount = 2,
                State = ResourceState.OK,
                ResourceType = ResourceType.Engine
            },
            new Resource()
            {
                Name = "Топливо",
                Amount = 22,
                State = ResourceState.OK,
                ResourceType = ResourceType.Fuel
            }
        };

        _shipRepository.SaveChanges();

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="shipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public ShipDTO GetShip(Guid shipId)
    {
        _logger.LogInformation("Get space ship by id {id}", shipId);

        Ship ship = GetRepositoryShip(shipId);

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="spaceshipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public ShipDTO? GetShips()
    {
        _logger.LogInformation("Get all space ships");

        List<Ship> ship = _shipRepository.GetAll();

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <summary>
    /// Изменение метрик существующего корабля.
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public ShipDTO UpdateShip(Guid shipId, ShipDTO spaceShipDTO)
    {
        _logger.LogInformation(
            "Update space ship with id {id}: {request}",
            shipId,
            JsonSerializer.Serialize(spaceShipDTO));

        Ship ship = UpdateRepositoryShip(shipId, spaceShipDTO);

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <summary>
    /// Удалить корабль.
    /// </summary>
    public bool DeleteShip(Guid shipId)
    {
        _logger.LogInformation("Delete space ship with id {id}", shipId);

        return _shipRepository.Delete(shipId);
    }

    #endregion

    #region private methods

    /// <summary>
    /// Get ship from repository.
    /// </summary>
    /// <exception cref="NotFoundException">
    /// In case the ship is not found by the repository.
    /// </exception>
    private Ship GetRepositoryShip(Guid shipId)
    {
        Ship? ship = _shipRepository.Get(shipId);

        if (ship == null)
        {
            _logger.LogInformation("Space ship with ID {shipId} not found.", shipId);

            throw new NotFoundException($"Ship with ID {shipId} not found.");
        }

        return ship;
    }

    /// <summary>
    /// Update ship in repository.
    /// </summary>
    /// <exception cref="NotFoundException">
    /// In case the ship is not found by the repository.
    /// </exception>
    /// <exception cref="NotModifiedException">
    /// In case no changes are requested.
    /// </exception>
    private Ship UpdateRepositoryShip(
        Guid shipId,
        ShipDTO shipRequest)
    {
        Ship currentShip = GetRepositoryShip(shipId);

        bool updateRequested = false;

        if (shipRequest.Name != null && shipRequest.Name != currentShip.Name)
        {
            updateRequested = true;
            currentShip.Name = shipRequest.Name;
        }

        if (shipRequest.Step != currentShip.Step)
        {
            updateRequested = true;
            currentShip.Step = shipRequest.Step;
        }

        if (!updateRequested)
        {
            throw new NotModifiedException();
        }

        _shipRepository.Update(currentShip, saveChanges: true);

        return currentShip;
    }

    #endregion
}