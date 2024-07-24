using AutoMapper;
using GameController.Services.Exceptions;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

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
    public SpaceShipDTO CreateShip(SpaceShipDTO spaceShipDTO)
    {
        Ship shipRequest = _mapper.Map<Ship>(spaceShipDTO);

        Ship ship = _shipRepository.Create(shipRequest);

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="shipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO? GetShip(Guid shipId)
    {
        Ship ship = GetRepositoryShip(shipId);

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Получить метрики корабля.
    /// </summary>
    /// <param name="shipId">ID корабля</param>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO? GetShips(Guid shipId)
    {
        List<Ship> ship = _shipRepository.GetAll();

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Изменение метрик существующего корабля.
    /// </summary>
    /// <returns>Метрики корабля</returns>
    public SpaceShipDTO UpdateShip(Guid shipId, SpaceShipDTO spaceShipDTO)
    {
        Ship ship = UpdateRepositoryShip(shipId, spaceShipDTO);

        return _mapper.Map<SpaceShipDTO>(ship);
    }

    /// <summary>
    /// Удалить корабль.
    /// </summary>
    public bool DeleteShip(Guid shipTypeId)
    {
        return _shipRepository.Delete(shipTypeId);
    }

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public void ProcessNewDay(Guid id)
    {
        _logger.LogInformation("Process new day for ship with id {id}", id);

        var ship = _shipRepository.Get(id);
        ship.Step++;
        _shipRepository.Update(ship);
        return;
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
            throw new NotFoundException($"User with ID {shipId} not found.");
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

        if (shipRequest.Name != null && shipRequest.Name != currentShip.Name)
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
