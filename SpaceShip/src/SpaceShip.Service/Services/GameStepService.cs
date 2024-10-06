using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для обработки события нового игрового дня (шага игры).
/// Регистрируется как ScopedService для работы с обработчиками сообщений из очереди RabbitMQ.
/// </summary>
public class GameStepService : IGameStepService
{
    #region private fields

    private readonly IShipService _shipService;

    private readonly ILogger _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameStepService(
        IShipService shipService,
        IMapper mapper,
        ILogger<GameStepService> logger)
    {
        _shipService = shipService;

        _logger = logger;
    }

    #endregion

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public async Task<ShipDTO> ProcessNewDayAsync(Guid id)
    {
        _logger.LogInformation("Process new day for ship with id {id}", id);

        ShipDTO ship = _shipService.GetShip(id);

        ship.Step++;

        return _shipService.UpdateShip(id, ship);
    }
}
