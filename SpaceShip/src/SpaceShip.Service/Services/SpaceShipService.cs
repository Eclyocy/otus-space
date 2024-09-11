using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для работы с сущностью "Корабль".
/// </summary>
public class SpaceShipService : IShipService
{
    #region private fields

    private readonly ISpaceshipRepository _shipRepository;

    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор.
    /// </summary>
    public SpaceShipService(
        ISpaceshipRepository shipRepository,
        IMapper mapper,
        ILogger<SpaceShipService> logger)
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
    public SpaceShipDTO CreateShip()
    {
        _logger.LogInformation("Create space ship");

        Ship ship = _shipRepository.Create();

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="shipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO? GetShip(Guid shipId)
    {
        _logger.LogInformation("Get space ship by id {id}", shipId);

        Ship ship = GetRepositoryShip(shipId);

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="spaceshipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO? GetShips()
    {
        _logger.LogInformation("Get all space ships");

        List<Ship> ship = _shipRepository.GetAll();

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Метод для проверки существования корабля.
    /// </summary>
    /// <param name="shipId">ID корабля.</param>
    /// <param name="ship">возвращаемые метрики корабля, если он найден.</param>
    /// <returns>true если корабль найден, false в противном случае.</returns>
    public bool TryGetShip(Guid shipId, out SpaceShipDTO? ship)
    {
        Ship? repoShip = _shipRepository.Get(shipId);

        if (repoShip is null)
        {
            ship = null;
            return false;
        }

        ship = _mapper.Map<SpaceShipDTO>(repoShip);
        return true;
    }

    /// <summary>
    /// Изменение метрик существующего корабля.
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO UpdateShip(Guid shipId, SpaceShipDTO spaceShipDTO)
    {
        _logger.LogInformation(
            "Update space ship with id {id}: {request}",
            shipId,
            JsonSerializer.Serialize(spaceShipDTO));

        Ship ship = UpdateRepositoryShip(shipId, spaceShipDTO);

        return _mapper.Map<SpaceShipDTO>(ship);
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
        SpaceShipDTO shipRequest)
    {
        Ship currentShip = GetRepositoryShip(shipId);

        bool updateRequested = false;

        // TODO: перейти на валидатор.
        if ((shipRequest.Name != null && shipRequest.Name != currentShip.Name) || shipRequest.Step != currentShip.Step || shipRequest.State != currentShip.State)
        {
            updateRequested = true;
            currentShip.Name = shipRequest.Name;
        }

        if (!updateRequested)
        {
            throw new NotModifiedException();
        }

        _shipRepository.Update(currentShip); // updates entity in-place

        return currentShip;
    }

    #endregion
}
