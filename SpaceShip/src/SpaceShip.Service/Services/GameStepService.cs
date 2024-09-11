using Microsoft.Extensions.Logging;
using SpaceShip.Notifications;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для обработки события нового игрового дня (шага игры).
/// Регистрируется как ScopedService для работы с обработчиками сообщений из очереди RabbitMQ.
/// </summary>
public class GameStepService : IGameStepService
{
    #region private fields

    private readonly ILogger _logger;
    private readonly IShipService _shipService;
    private readonly INotificationsProvider _notificationsProvider;

    #endregion

    #region constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public GameStepService(
        IShipService shipService,
        ILogger<SpaceShipService> logger,
        INotificationsProvider notificationsProvider)
    {
        _shipService = shipService;
        _logger = logger;
        _notificationsProvider = notificationsProvider;
    }

    #endregion

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public async Task ProcessNewDayAsync(Guid id)
    {
        _logger.LogInformation("Process new day for ship with id {id}", id);

        var ship = _shipService.GetShip(id);

        if (ship != null)
        {
            ship.Step++;

            _logger.LogInformation("Update ship {id} in repository. Set step {step} ", id, ship.Step);
            _shipService.UpdateShip(id, ship);

            _logger.LogInformation("Try to notify about ship {id} changes", id);
            await _notificationsProvider.SendAsync(id, ship);
        }
    }
}
